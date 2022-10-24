import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import {
    Loading,
    CreateButton,
    ErrorCard,
    Title,
    PaginatedTable
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const PartFailErrorTypes = ({ loading, itemError, history, items }) => {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });
    const [rowsToDisplay, setRowsToDisplay] = useState([]);

    const formatDate = date => `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;

    useEffect(() => {
        const rows = items
            ? items.map(el => ({
                  type: `${el.errorType}`,
                  description: el.dateInvalid ? formatDate(new Date(el.dateInvalid)) : null,
                  links: el.links
              }))
            : null;

        if (!rows || rows.length === 0) {
            setRowsToDisplay([]);
        } else {
            setRowsToDisplay(
                rows
                    .slice(
                        pageOptions.currentPage * pageOptions.rowsPerPage,
                        pageOptions.currentPage * pageOptions.rowsPerPage + pageOptions.rowsPerPage
                    )
                    .map(t => ({ ...t, id: t.type }))
            );
        }
    }, [
        pageOptions.currentPage,
        pageOptions.rowsPerPage,
        pageOptions.orderBy,
        pageOptions.orderAscending,
        items
    ]);

    const handleRowLinkClick = href => history.push(href);

    const columns = {
        failType: 'Type',
        dateInvalid: 'Date Invalid'
    };

    return (
        <Page>
            <Title text="Part Fail Error Types" />
            {itemError && <ErrorCard errorMessage={itemError?.statusText} />}
            {loading ? (
                <Loading />
            ) : (
                <>
                    <>
                        <CreateButton createUrl="/production/quality/part-fail-error-types/create" />
                    </>

                    <PaginatedTable
                        columns={columns}
                        sortable
                        handleRowLinkClick={handleRowLinkClick}
                        rows={rowsToDisplay}
                        pageOptions={pageOptions}
                        setPageOptions={setPageOptions}
                        totalItemCount={items ? items.length : 0}
                    />
                </>
            )}
        </Page>
    );
};

PartFailErrorTypes.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({ statusText: PropTypes.string })
};

PartFailErrorTypes.defaultProps = {
    itemError: null,
    items: []
};

export default PartFailErrorTypes;
