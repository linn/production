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
    SearchInputField,
    Dropdown
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const ViewProductionTriggerLevels = ({ loading, itemError, history, items, fetchItems, cits }) => {
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
                  citCode: `${el.citCode} - ${cits.find(x => x.code === el.citCode)?.name} `,
                  links: el.links,
                  id: el.partNumber,
                  overrideTriggerLevel: el.overrideTriggerLevel,
                  VariableTriggerLevel: el.variableTriggerLevel
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
            );
        }
    }, [
        pageOptions.currentPage,
        pageOptions.rowsPerPage,
        pageOptions.orderBy,
        pageOptions.orderAscending,
        items,
        cits
    ]);

    const [searchTerm, setSearchTerm] = useState(null);
    const [partSearch, setPartSearch] = useState('');
    const [citSearch, setCitSearch] = useState('');
    const [overrideSearch, setOverrideSearch] = useState(null);
    const [autoSearch, setAutoSearch] = useState(null);

    useSearch(fetchItems, searchTerm, null, 'searchTerm');

    const handlePartSearchChange = (...args) => {
        setPartSearch(args[1]);
        setSearchTerm(`${args[1]};${citSearch};${overrideSearch};${autoSearch}`);
    };

    const handleCitSearchChange = (...args) => {
        setCitSearch(args[1]);
        setSearchTerm(`${partSearch};${args[1]};${overrideSearch};${autoSearch}`);
    };

    const handleOverrideSearchChange = (...args) => {
        setOverrideSearch(args[1]);
        setSearchTerm(`${partSearch};${citSearch};${args[1]};${autoSearch}`);
    };

    const handleAutoSearchChange = (...args) => {
        setAutoSearch(args[1]);
        setSearchTerm(`${partSearch};${citSearch};${overrideSearch};${args[1]}`);
    };

    const handleRowLinkClick = href => history.push(href);

    const columns = {
        partNumber: 'Part Number',
        description: 'Description',
        citCode: 'Cit',
        overrideTriggerLevel: 'Override Trigger Level',
        VariableTriggerLevel: 'Auto Trigger Level'
    };

    return (
        <Page>
            <Title text="Trigger Levels" />
            {itemError && <ErrorCard errorMessage={itemError.statusText} />}

            <Fragment>
                <CreateButton createUrl="/production/maintenance/production-trigger-levels/create" />
            </Fragment>
            <Grid item xs={12} container>
                <Grid item xs={4}>
                    <SearchInputField
                        label="Part Number"
                        fullWidth
                        placeholder="search.."
                        onChange={handlePartSearchChange}
                        propertyName="partSearchTerm"
                        type="text"
                        value={partSearch}
                    />
                </Grid>
                <Grid item xs={1} />
                <Grid item xs={2}>
                    <Dropdown
                        onChange={handleCitSearchChange}
                        items={cits.map(cit => ({
                            ...cit,
                            id: cit.code,
                            displayText: `${cit.code} (${cit.name})`
                        }))}
                        value={citSearch}
                        propertyName="citSearch"
                        required
                        fullWidth
                        label="Cit"
                        allowNoValue
                    />
                </Grid>
                <Grid item xs={1} />
                <Grid item xs={1}>
                    <SearchInputField
                        label="Override Trigger >"
                        fullWidth
                        // placeholder="search.."
                        onChange={handleOverrideSearchChange}
                        propertyName="overrideSearchTerm"
                        type="number"
                        value={overrideSearch}
                    />
                </Grid>
                <Grid item xs={1} />
                <Grid item xs={1}>
                    <SearchInputField
                        label="Auto Trigger >"
                        fullWidth
                        // placeholder="search.."
                        onChange={handleAutoSearchChange}
                        propertyName="autoSearchTerm"
                        type="number"
                        value={autoSearch}
                    />
                </Grid>
            </Grid>
            {loading ? (
                <Loading />
            ) : (
                <Fragment>
                    {rowsToDisplay.length > 0 && (
                        <PaginatedTable
                            columns={columns}
                            handleRowLinkClick={handleRowLinkClick}
                            rows={rowsToDisplay}
                            pageOptions={pageOptions}
                            setPageOptions={setPageOptions}
                            totalItemCount={items ? items.length : 0}
                            expandable={false}
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
    fetchItems: PropTypes.func.isRequired,
    cits: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.string,
            code: PropTypes.string
        })
    ).isRequired
};

ViewProductionTriggerLevels.defaultProps = {
    itemError: null,
    items: []
};

export default ViewProductionTriggerLevels;
