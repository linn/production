import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import {
    Loading,
    CreateButton,
    ErrorCard,
    Title,
    PaginatedTable,
    useSearch,
    SearchInputField,
    Dropdown,
    utilities,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const ViewProductionTriggerLevels = ({
    loading,
    itemError,
    history,
    items,
    fetchItems,
    cits,
    applicationState,
    editStatus
}) => {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });
    const [rowsToDisplay, setRowsToDisplay] = useState([]);
    const [allowedToCreate, setAllowedToCreate] = useState(false);
    const [snackbarVisible, setSnackbarVisible] = useState(editStatus === 'deleted');

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
                  routeCode: el.routeCode,
                  workStationName: el.workStationName,
                  links: el.links,
                  id: el.partNumber,
                  overrideTriggerLevel: el.overrideTriggerLevel,
                  variableTriggerLevel: el.variableTriggerLevel,
                  kanbanSize: el.kanbanSize
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

        setAllowedToCreate(utilities.getHref(applicationState, 'edit') !== null);
    }, [
        pageOptions.currentPage,
        pageOptions.rowsPerPage,
        pageOptions.orderBy,
        pageOptions.orderAscending,
        items,
        cits,
        applicationState
    ]);

    const [searchTerm, setSearchTerm] = useState(null);
    const [partSearch, setPartSearch] = useState('');
    const [citSearch, setCitSearch] = useState('');
    const [overrideSearch, setOverrideSearch] = useState(null);
    const [autoSearch, setAutoSearch] = useState(null);
    const [workStationSearch, setWorkStationSearch] = useState('');

    useSearch(fetchItems, searchTerm, null, 'searchTerm');

    const handlePartSearchChange = (...args) => {
        setPartSearch(args[1]);
        setSearchTerm(`${args[1]};${citSearch};${overrideSearch};${autoSearch}`);
        setSearchTerm(
            `${args[1]}&citSearchTerm=${citSearch}&overrideSearchTerm=${overrideSearch}&autoSearchTerm=${autoSearch}&workStationSearchTerm=${workStationSearch}`
        );
    };

    const handleCitSearchChange = (...args) => {
        setCitSearch(args[1]);
        setSearchTerm(
            `${partSearch}&citSearchTerm=${args[1]}&overrideSearchTerm=${overrideSearch}&autoSearchTerm=${autoSearch}&workStationSearchTerm=${workStationSearch}`
        );
    };

    const handleOverrideSearchChange = (...args) => {
        setOverrideSearch(args[1]);
        setSearchTerm(
            `${partSearch}&citSearchTerm=${citSearch}&overrideSearchTerm=${args[1]}&autoSearchTerm=${autoSearch}&workStationSearchTerm=${workStationSearch}`
        );
    };

    const handleAutoSearchChange = (...args) => {
        setAutoSearch(args[1]);
        setSearchTerm(
            `${partSearch}&citSearchTerm=${citSearch}&overrideSearchTerm=${overrideSearch}&autoSearchTerm=${args[1]}&workStationSearchTerm=${workStationSearch}`
        );
    };

    const handleWorkStationSearchChange = (...args) => {
        setWorkStationSearch(args[1]);
        setSearchTerm(
            `${partSearch}&citSearchTerm=${citSearch}&overrideSearchTerm=${overrideSearch}&autoSearchTerm=${autoSearch}&workStationSearchTerm=${args[1]}`
        );
    };
    const handleRowLinkClick = href => history.push(href);

    const columns = {
        partNumber: 'Part Number',
        description: 'Description',
        citCode: 'Cit',
        routeCode: 'Route Code',
        workStationName: 'Work Station',
        overrideTriggerLevel: 'Override Trigger Level',
        VariableTriggerLevel: 'Auto Trigger Level',
        kanbanSize: 'Kanban Size'
    };

    const handleBPClick = () => {
        history.push('/production/maintenance/build-plans');
    };

    return (
        <Page>
            <Title text="Trigger Levels" />
            {itemError && <ErrorCard errorMessage={itemError?.statusText} />}

            {allowedToCreate && (
                <>
                    <CreateButton createUrl="/production/maintenance/production-trigger-levels/create" />
                </>
            )}

            <SnackbarMessage
                visible={snackbarVisible}
                onClose={() => setSnackbarVisible(false)}
                message="Deletion Successful"
            />

            <Grid item xs={12} container>
                <Grid item xs={3}>
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
                        items={cits
                            .map(cit => ({
                                ...cit,
                                id: cit.code,
                                displayText: `${cit.code} (${cit.name})`
                            }))
                            .sort((a, b) => (a.code > b.code ? 1 : -1))}
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
                        label="Work Station"
                        fullWidth
                        onChange={handleWorkStationSearchChange}
                        propertyName="workStatonSearchTerm"
                        type="text"
                        value={workStationSearch}
                    />
                </Grid>
                <Grid item xs={1} />
                <Grid item xs={1}>
                    <SearchInputField
                        label="Override Trigger >"
                        fullWidth
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
                <>
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
                </>
            )}
            <Grid container>
                <Grid item xs={12}>
                    <Button
                        color="primary"
                        variant="contained"
                        onClick={handleBPClick}
                        style={{ float: 'right', marginTop: '20px' }}
                    >
                        Build Plans
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
};

ViewProductionTriggerLevels.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({ statusText: PropTypes.string }),
    fetchItems: PropTypes.func.isRequired,
    cits: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.string,
            code: PropTypes.string
        })
    ).isRequired,
    applicationState: PropTypes.shape({ links: PropTypes.arrayOf(PropTypes.shape({})) }),
    editStatus: PropTypes.string
};

ViewProductionTriggerLevels.defaultProps = {
    itemError: null,
    items: [],
    applicationState: null,
    editStatus: ''
};

export default ViewProductionTriggerLevels;
