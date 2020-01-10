import React, { Fragment, useState, useEffect, useCallback } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    utilities,
    Dropdown,
    Typeahead,
    CreateButton
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function TriggerLevel({
    editStatus,
    itemErrors,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    manufacturingRoutes,
    cits,
    setSnackbarVisible,
    employees,
    workStations,
    partsSearchResults,
    searchParts,
    partsSearchLoading,
    clearPartsSearch,
    applicationState,
    appStateLoading
}) {
    const [triggerLevel, setTriggerLevel] = useState({});
    const [prevTriggerLevel, setPrevTriggerLevel] = useState({});
    const [allowedToEdit, setAllowedToEdit] = useState(false);
    const [allowedToCreate, setallowedToCreate] = useState(false);
    const [dialogOpen, setdialogOpen] = useState(true);

    const creating = useCallback(() => editStatus === 'create', [editStatus]);
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (creating()) {
            setAllowedToEdit(utilities.getHref(applicationState, 'edit') !== null);
        } else {
            setAllowedToEdit(utilities.getHref(item, 'edit') !== null);
            setallowedToCreate(utilities.getHref(item, 'edit') !== null);
        }
    }, [applicationState, item, creating]);

    useEffect(() => {
        if (item !== prevTriggerLevel) {
            setTriggerLevel(item);
            setPrevTriggerLevel(item);
        }
    }, [item, prevTriggerLevel]);

    const partNumberInvalid = () => !triggerLevel.partNumber;
    const descriptionInvalid = () => !triggerLevel.description;
    const citCodeInvalid = () => !triggerLevel.citCode;
    const kanbanSizeInvalid = () =>
        !(triggerLevel.kanbanSize != null && triggerLevel.kanbanSize >= 0);
    const maximumKanbansInvalid = () =>
        !(triggerLevel.maximumKanbans != null && triggerLevel.maximumKanbans >= 0);

    const inputInvalid = () =>
        partNumberInvalid() ||
        descriptionInvalid() ||
        citCodeInvalid() ||
        kanbanSizeInvalid() ||
        maximumKanbansInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, triggerLevel);
            setEditStatus('view');
        } else if (creating()) {
            addItem(triggerLevel);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        if (!creating()) {
            setEditStatus('view');
        }
        setTriggerLevel(item);
    };

    const handleBackClick = () => {
        history.push('/production/maintenance/production-trigger-levels/');
    };

    const handleResourceFieldChange = (propertyName, newValue) => {
        setTriggerLevel({ ...triggerLevel, [propertyName]: newValue });
        if (viewing()) {
            setEditStatus('edit');
        }
    };

    const handlePartNoChange = newValue => {
        setTriggerLevel({
            ...triggerLevel,
            partNumber: newValue.partNumber,
            description: newValue.description
        });
    };

    const handleOpenDialogRequest = () => {
        setDialogOpen(true);
    };

    const handleDelete = () => {
        alert('deletttted');
    }

    const temporaryItems = [{ displayText: 'Yes', id: 'Y' }];

    return (
        <Fragment>
            <Grid container alignItems="center" justify="center">
                <Grid xs={6} item>
                    <Page>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                {allowedToCreate && (
                                    <Fragment>
                                        <CreateButton createUrl="/production/maintenance/production-trigger-levels/create" />
                                    </Fragment>
                                )}
                                {creating() ? (
                                    <Title text="Create Production Trigger Level" />
                                ) : (
                                    <Title text="Production Trigger Level" />
                                )}
                            </Grid>

                            {itemErrors && (
                                <Grid item xs={12}>
                                    <ErrorCard errorMessage={itemErrors.statusText} />
                                </Grid>
                            )}
                            {loading || appStateLoading || !triggerLevel ? (
                                <Grid item xs={12}>
                                    <Loading />
                                </Grid>
                            ) : (
                                <Fragment>
                                    <SnackbarMessage
                                        visible={snackbarVisible}
                                        onClose={() => setSnackbarVisible(false)}
                                        message="Save Successful"
                                    />
                                    {!allowedToEdit && !creating() && (
                                        <Grid item xs={12}>
                                            <ErrorCard errorMessage="You are not authorised to update trigger levels" />
                                        </Grid>
                                    )}
                                    <Grid item xs={6}>
                                        {!creating() && (
                                            <InputField
                                                fullWidth
                                                disabled={!creating() || !allowedToEdit}
                                                value={triggerLevel.partNumber}
                                                label="Part Number"
                                                maxLength={10}
                                                helperText="This field cannot be changed"
                                                required
                                                onChange={handleResourceFieldChange}
                                                propertyName="partNumber"
                                            />
                                        )}
                                        {creating() && allowedToEdit && (
                                            <Typeahead
                                                onSelect={newValue => {
                                                    handlePartNoChange(newValue);
                                                }}
                                                propertyName="partNumber"
                                                label="Part Number"
                                                modal
                                                items={partsSearchResults}
                                                value={triggerLevel.partNumber}
                                                loading={partsSearchLoading}
                                                fetchItems={searchParts}
                                                links={false}
                                                clearSearch={() => clearPartsSearch}
                                                placeholder="Search For Part Number"
                                            />
                                        )}
                                    </Grid>
                                    {!allowedToEdit && creating() && (
                                        <Grid item xs={12}>
                                            <ErrorCard errorMessage="You are not authorised to create trigger levels" />
                                        </Grid>
                                    )}
                                    <Grid item xs={12}>
                                        <InputField
                                            value={triggerLevel.description}
                                            label="Description"
                                            fullWidth
                                            propertyName="description"
                                            disabled
                                            helperText="This field cannot be changed - description comes from part number"
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Grid item xs={6}>
                                            <Dropdown
                                                onChange={handleResourceFieldChange}
                                                items={cits.map(cit => ({
                                                    ...cit,
                                                    id: cit.code,
                                                    displayText: `${cit.code} (${cit.name})`
                                                }))}
                                                value={triggerLevel.citCode}
                                                propertyName="citCode"
                                                helperText={
                                                    citCodeInvalid() ? 'This field is required' : ''
                                                }
                                                required
                                                fullWidth
                                                label="CIT Code"
                                                allowNoValue
                                                disabled={!allowedToEdit}
                                            />
                                        </Grid>
                                    </Grid>

                                    {!creating() && (
                                        <Grid item xs={6}>
                                            <InputField
                                                value={triggerLevel.variableTriggerLevel}
                                                label="Auto Trigger Level"
                                                type="number"
                                                maxLength={2}
                                                fullWidth
                                                propertyName="variableTriggerLevel"
                                                disabled
                                            />
                                        </Grid>
                                    )}
                                    <Grid item xs={creating() ? 12 : 6}>
                                        <Grid item xs={creating() ? 6 : 12}>
                                            <InputField
                                                value={triggerLevel.overrideTriggerLevel}
                                                label="Override Trigger Level"
                                                maxLength={2}
                                                type="number"
                                                fullWidth
                                                onChange={handleResourceFieldChange}
                                                propertyName="overrideTriggerLevel"
                                                disabled={!allowedToEdit}
                                            />
                                        </Grid>
                                    </Grid>
                                    <Grid item xs={6}>
                                        <InputField
                                            value={triggerLevel.kanbanSize}
                                            label="Kanban Size"
                                            maxLength={2}
                                            type="number"
                                            fullWidth
                                            helperText={
                                                kanbanSizeInvalid() ? 'This field is required' : ''
                                            }
                                            required
                                            onChange={handleResourceFieldChange}
                                            propertyName="kanbanSize"
                                            disabled={!allowedToEdit}
                                        />
                                    </Grid>
                                    <Grid item xs={6}>
                                        <InputField
                                            value={triggerLevel.maximumKanbans}
                                            label="Maximum Kanbans"
                                            type="number"
                                            maxLength={2}
                                            fullWidth
                                            helperText={
                                                maximumKanbansInvalid()
                                                    ? 'This field is required'
                                                    : ''
                                            }
                                            required
                                            onChange={handleResourceFieldChange}
                                            propertyName="maximumKanbans"
                                            disabled={!allowedToEdit}
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Grid item xs={6}>
                                            <Dropdown
                                                items={manufacturingRoutes.map(
                                                    route => route.routeCode
                                                )}
                                                propertyName="routeCode"
                                                fullWidth
                                                value={triggerLevel.routeCode}
                                                label="Route Code"
                                                allowNoValue
                                                onChange={handleResourceFieldChange}
                                                disabled={!allowedToEdit}
                                            />
                                        </Grid>
                                    </Grid>
                                    <Grid item xs={6}>
                                        <Dropdown
                                            items={workStations.map(ws => ({
                                                ...ws,
                                                id: ws.workStationCode,
                                                displayText: `${ws.workStationCode} - ${ws.description}`
                                            }))}
                                            propertyName="workStationName"
                                            fullWidth
                                            value={triggerLevel.workStationName}
                                            label="Work Station"
                                            allowNoValue
                                            onChange={handleResourceFieldChange}
                                            disabled={!allowedToEdit}
                                        />
                                    </Grid>
                                    <Grid item xs={6}>
                                        <Dropdown
                                            items={employees.map(e => ({
                                                displayText: `${e.fullName} (${e.id})`,
                                                id: e.id
                                            }))}
                                            propertyName="engineerId"
                                            fullWidth
                                            value={triggerLevel.engineerId}
                                            label="engineerId"
                                            allowNoValue
                                            onChange={handleResourceFieldChange}
                                            disabled={!allowedToEdit}
                                            type="number"
                                        />
                                    </Grid>
                                    <Grid item xs={2}>
                                        <Dropdown
                                            items={temporaryItems}
                                            propertyName="temporary"
                                            fullWidth
                                            value={triggerLevel.temporary}
                                            label="temporary"
                                            onChange={handleResourceFieldChange}
                                            allowNoValue
                                            disabled={!allowedToEdit}
                                        />
                                    </Grid>
                                    <Grid item xs={10}>
                                        <InputField
                                            value={triggerLevel.story}
                                            label="Story"
                                            fullWidth
                                            onChange={handleResourceFieldChange}
                                            propertyName="story"
                                            disabled={!allowedToEdit}
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Button
                                            color="default"
                                            variant="contained"
                                            style={{ float: 'left' }}                                            
                                            onClick={handleOpenDialogRequest}
                                        >
                                            Delete
                                        </Button>
                                        <SaveBackCancelButtons
                                            saveDisabled={viewing() || inputInvalid()}
                                            saveClick={handleSaveClick}
                                            cancelClick={handleCancelClick}
                                            backClick={handleBackClick}
                                        />
                                    </Grid>
                                    <Dialog
                                        open={dialogOpen}
                                        onClose={}
                                        fullWidth
                                        maxWidth="md"
                                    >
                                        <span>
                                            Are you sure you want to delete this hingmy???? :O
                                        </span>
                                        <Button
                                            color="default"
                                            variant="contained"
                                            onClick={handleDelete}
                                        >
                                            Aye get it away
                                        </Button>
                                        <Button
                                            color="default"
                                            variant="contained"
                                            onClick={handleOpenDialogRequest}
                                        >
                                            Cancel
                                        </Button>
                                    </Dialog>
                                </Fragment>
                            )}
                        </Grid>
                    </Page>
                </Grid>
            </Grid>
        </Fragment>
    );
}

TriggerLevel.propTypes = {
    item: PropTypes.shape({
        partNumber: PropTypes.string,
        description: PropTypes.string,
        citCode: PropTypes.string,
        bomLevel: PropTypes.number,
        kanbanSize: PropTypes.number,
        maximumKanbans: PropTypes.number,
        overrideTriggerLevel: PropTypes.number,
        triggerLevel: PropTypes.number,
        variableTriggerLevel: PropTypes.number,
        workStationName: PropTypes.string,
        temporary: PropTypes.string,
        engineerId: PropTypes.number,
        story: PropTypes.string,
        routeCode: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemErrors: PropTypes.shape({}),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    partsSearchResults: PropTypes.arrayOf(
        PropTypes.shape({
            partNumber: PropTypes.string
        })
    ),
    manufacturingRoutes: PropTypes.arrayOf(
        PropTypes.shape({
            routeCode: PropTypes.string
        })
    ).isRequired,
    cits: PropTypes.arrayOf(
        PropTypes.shape({
            code: PropTypes.string
        })
    ),
    employees: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            fullName: PropTypes.string
        })
    ),
    workStations: PropTypes.arrayOf(
        PropTypes.shape({
            workStationCode: PropTypes.string,
            description: PropTypes.string
        })
    ),
    searchParts: PropTypes.func,
    partsSearchLoading: PropTypes.bool,
    clearPartsSearch: PropTypes.func,
    applicationState: PropTypes.shape({ links: PropTypes.arrayOf(PropTypes.shape({})) }),
    appStateLoading: PropTypes.bool
};

TriggerLevel.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemErrors: null,
    itemId: null,
    partsSearchResults: null,
    workStations: [],
    cits: [],
    employees: [],
    searchParts: null,
    partsSearchLoading: false,
    clearPartsSearch: null,
    applicationState: null,
    appStateLoading: false
};

export default TriggerLevel;
