import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    Loading,
    CreateButton,
    ErrorCard,
    Title,
    PaginatedTable,
    useSearch,
    SearchInputField
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const ViewManufacturingRoutes = ({ loading, itemError, history, items, fetchItems }) => {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });
    const [rowsToDisplay, setRowsToDisplay] = useState([]);

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
        const rows = items
            ? items.map(el => ({
                  routeCode: el.routeCode,
                  description: el.description,
                  links: el.links
              }))
            : [];

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
                    .map(r => ({ ...r, id: r.routeCode }))
            );
        }
    }, [
        pageOptions.currentPage,
        pageOptions.rowsPerPage,
        pageOptions.orderBy,
        pageOptions.orderAscending,
        items
    ]);

    const [searchTerm, setSearchTerm] = useState(null);

    useSearch(fetchItems, searchTerm, null, 'searchTerm');

    const handleSearchTermChange = (...args) => {
        setSearchTerm(args[1]);
    };

    const handleRowLinkClick = href => history.push(href);

    const columns = {
        routeCode: 'Route Code',
        description: 'Description'
    };

    return (
        <Page>
            <Title text="Manufacturing Routes" />
            {itemError && <ErrorCard errorMessage={itemError.statusText} />}

            <>
                <CreateButton createUrl="/production/resources/manufacturing-routes/create" />
            </>
            <Grid item xs={8}>
                <SearchInputField
                    label="Route Code"
                    fullWidth
                    placeholder="search.."
                    onChange={handleSearchTermChange}
                    propertyName="searchTerm"
                    type="text"
                    value={searchTerm}
                />
            </Grid>
            {loading ? (
                <Loading />
            ) : (
                <>
                    {rowsToDisplay.length > 0 && (
                        <PaginatedTable
                            columns={columns}
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

ViewManufacturingRoutes.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({}),
    fetchItems: PropTypes.func.isRequired
};

ViewManufacturingRoutes.defaultProps = {
    itemError: null,
    items: []
};

export default ViewManufacturingRoutes;
