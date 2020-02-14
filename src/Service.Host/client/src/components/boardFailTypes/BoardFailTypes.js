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

const BoardFailTypes = ({ loading, itemError, history, items }) => {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });
    const [rowsToDisplay, setRowsToDisplay] = useState([]);

    useEffect(() => {
        const rows = items
            ? items.map(el => ({
                  type: `${el.failType}`,
                  description: el.description,
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
                    .map(r => ({ ...r, id: r.type }))
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
        description: 'Description'
    };

    return (
        <Page>
            <Title text="Board Fail Types" />
            {itemError && <ErrorCard errorMessage={itemError?.statusText} />}
            {loading ? (
                <Loading />
            ) : (
                <>
                    <>
                        <CreateButton createUrl="/production/resources/board-fail-types/create" />
                    </>
                    {rowsToDisplay.length > 0 && (
                        <PaginatedTable
                            columns={columns}
                            sortable
                            handleRowLinkClick={handleRowLinkClick}
                            rows={rowsToDisplay}
                            pageOptions={pageOptions}
                            setPageOptions={setPageOptions}
                            totalItemCount={items ? items.length : 0}
                        />
                    )}
                </>
            )}
        </Page>
    );
};

BoardFailTypes.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({})
};

BoardFailTypes.defaultProps = {
    itemError: null,
    items: []
};

export default BoardFailTypes;
