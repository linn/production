﻿import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import {
    Loading,
    CreateButton,
    ErrorCard,
    Title,
    PaginatedTable
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function AteFaultCodes({ loading, errorMessage, history, items }) {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });
    const [rows, setRows] = useState([]);
    const [rowsToDisplay, setRowsToDisplay] = useState([]);
    useEffect(() => {
        setRows(
            items.map(el => ({
                faultCode: el.faultCode,
                description: el.description,
                links: el.links
            }))
        );
    }, [items]);

    useEffect(() => {
        const compare = (field, orderAscending) => (a, b) => {
            if (!field) {
                return 0;
            }

            if (a[field] < b[field]) {
                return orderAscending ? -1 : 1;
            }

            if (a[field] > b[field]) {
                return orderAscending ? 1 : -1;
            }

            return 0;
        };

        if (!rows || rows.length === 0) {
            setRowsToDisplay([]);
        } else {
            setRowsToDisplay(
                rows
                    .sort(compare(pageOptions.orderBy, pageOptions.orderAscending))
                    .slice(
                        pageOptions.currentPage * pageOptions.rowsPerPage,
                        pageOptions.currentPage * pageOptions.rowsPerPage + pageOptions.rowsPerPage
                    )
            );
        }
    }, [
        pageOptions.currentPage,
        pageOptions.rowsPerPage,
        pageOptions.orderBy,
        pageOptions.orderAscending,
        rows
    ]);

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
                    <PaginatedTable
                        columns={columns}
                        sortable
                        handleRowLinkClick={handleRowLinkClick}
                        rows={rowsToDisplay}
                        pageOptions={pageOptions}
                        setPageOptions={setPageOptions}
                        totalItemCount={rows.length}
                    />
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
