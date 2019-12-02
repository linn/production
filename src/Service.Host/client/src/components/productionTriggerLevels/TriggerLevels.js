import React, { Fragment, useState, useEffect } from 'react';
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

const ViewProductionTriggerLevels = ({ loading, itemError, history, items, fetchItems }) => {
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
                  partNumber: el.partNumber,
                  description: el.description,
                  citCode: el.citCode,
                  links: el.links
                //   bomLevel: el.bomLevel,
                //   faZoneType: el.faZoneType,
                //   kanbanSize: el.kanbanSize,
                //   maximumKanbans: el.maximumKanbans,
                //   overrideTriggerLevel: el.overrideTriggerLevel,
                //   triggerLevel: el.triggerLevel,
                //   variableTriggerLevel: el.variableTriggerLevel,
                //   workStation: el.workStation,
                //   temporary: el.temporary,
                //   engineerId: el.engineerId,
                //   story: el.story,
                //   routeCode: el.routeCode
                // links: el.links
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

    const handleRowLinkClick = href => {
        console.info(href);
        console.info(history);
        history.push(href);
    };

    const columns = {
        partNumber: 'Part Number',
        description: 'Description',
        citCode: 'Cit'
    };

    return (
        <Page>
            <Title text="Trigger Levels" />
            {itemError && <ErrorCard errorMessage={itemError.statusText} />}

            <Fragment>
                <CreateButton createUrl="/production/maintenance/production-trigger-levels/create" />
            </Fragment>
            <Grid item xs={8}>
                <SearchInputField
                    label="Part Number"
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
                <Fragment>
                    {rowsToDisplay.length > 0 && (
                        <PaginatedTable
                            columns={columns}
                            handleRowLinkClick={handleRowLinkClick}
                            rows={rowsToDisplay.map(row => ({ ...row, id: row.labelTypeCode }))}
                            pageOptions={pageOptions}
                            setPageOptions={setPageOptions}
                            totalItemCount={items ? items.length : 0}
                        />
                    )}
                </Fragment>
            )}
        </Page>
    );
};

ViewProductionTriggerLevels.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({}),
    fetchItems: PropTypes.func.isRequired
};

ViewProductionTriggerLevels.defaultProps = {
    itemError: null,
    items: []
};

export default ViewProductionTriggerLevels;
