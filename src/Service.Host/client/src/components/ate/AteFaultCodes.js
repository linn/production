import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import {
    Loading,
    CreateButton,
    ErrorCard,
    Title,
    PaginatedTable
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';
import PaginatedTable2 from '../PaginatedTable';

function AteFaultCodes({ loading, errorMessage, history, items }) {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });
    const [rowsToDisplay, setRowsToDisplay] = useState([]);
    const rows = items.map(el => ({
        faultCode: el.faultCode,
        description: el.description,
        links: el.links
    }));

    useEffect(() => {
        if (!rows || rows.length === 0) {
            setRowsToDisplay([]);
        } else {
            setRowsToDisplay(
                rows.slice(
                    pageOptions.currentPage * pageOptions.rowsPerPage,
                    pageOptions.currentPage * pageOptions.rowsPerPage + pageOptions.rowsPerPage
                )
            );
        }
    }, [pageOptions, rows]);

    const handleRowLinkClick = href => history.push(href);

    const columns = { faultCode: 'Fault Code', description: 'Description' };

    return (
        <Page>
            <Title text="ATE Fault Codes" />
            {errorMessage && <ErrorCard errorMessage={errorMessage} />}
            {loading ? (
                <Loading />
            ) : (
                <Fragment>
                    <CreateButton createUrl="/production/quality/ate/fault-codes/create" />
                    {rowsToDisplay.length === 0 ? (
                        ''
                    ) : (
                        <PaginatedTable2
                            columns={columns}
                            handleRowLinkClick={handleRowLinkClick}
                            rows={rowsToDisplay}
                            pageOptions={pageOptions}
                            setPageOptions={setPageOptions}
                            totalItemCount={rows.length}
                        />
                    )}
                </Fragment>
            )}
        </Page>
    );
}

AteFaultCodes.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    errorMessage: PropTypes.string
};

AteFaultCodes.defaultProps = {
    errorMessage: '',
    items: []
};

export default AteFaultCodes;
