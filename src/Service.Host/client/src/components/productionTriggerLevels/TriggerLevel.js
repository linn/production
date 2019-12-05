import React, { Fragment, useState, useEffect, useCallback } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    utilities,
    Dropdown
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
    parts,
    manufacturingRoutes,
    cits,
    setSnackbarVisible,
    employees,
    getWorkStationsForCit,
    workStations
}) {
    const [triggerLevel, setTriggerLevel] = useState({});
    const [prevTriggerLevel, setPrevTriggerLevel] = useState({});
    const [allowedToEdit, setAllowedToEdit] = useState(false);

    const creating = useCallback(() => editStatus === 'create', [editStatus]);
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevTriggerLevel) {
            setTriggerLevel(item);
            setPrevTriggerLevel(item);
            if (item?.citCode) {
                getWorkStationsForCit('searchTerm', item.citCode);
            }
        }

        setAllowedToEdit(utilities.getHref(item, 'edit') !== null);
    }, [item, prevTriggerLevel, editStatus, creating, getWorkStationsForCit]);

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
        setEditStatus('view');
        setTriggerLevel(item);
        getWorkStationsForCit('searchTerm', item.citCode);
    };

    const handleBackClick = () => {
        history.push('/production/resources/manufacturing-routes/');
    };

    const handleResourceFieldChange = (propertyName, newValue) => {
        setTriggerLevel({ ...triggerLevel, [propertyName]: newValue });
        if (viewing()) {
            setEditStatus('edit');
        }
    };

    const handleCitChange = (propertyName, newValue) => {
        getWorkStationsForCit('searchTerm', newValue);
        handleResourceFieldChange(propertyName, newValue);
    };

    let partNumbers = [];
    if (creating()) {
        partNumbers = parts.map(part => part.partNumber);
    }

    const temporaryItems = [
        { displayText: 'Yes', id: 'Y' },
        { displayText: 'No', id: null }
    ];

    return (
        <Fragment>
            <Grid container alignItems="center" justify="center">
                <Grid xs={6} item>
                    <Page>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
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
                            {loading || !triggerLevel ? (
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
                                    <Grid item xs={6}>
                                        {!creating() && (
                                            <InputField
                                                fullWidth
                                                disabled={!creating()}
                                                value={triggerLevel.partNumber}
                                                label="Part Number"
                                                maxLength={10}
                                                helperText={
                                                    !creating()
                                                        ? 'This field cannot be changed'
                                                        : `${
                                                              partNumberInvalid()
                                                                  ? 'This field is required'
                                                                  : ''
                                                          }`
                                                }
                                                required
                                                onChange={handleResourceFieldChange}
                                                propertyName="partNumbers"
                                            />
                                        )}
                                        {creating() && (
                                            <Dropdown
                                                onChange={handleCitChange}
                                                items={partNumbers}
                                                value={triggerLevel.citCode}
                                                propertyName="citCode"
                                                helperText={
                                                    citCodeInvalid() ? 'This field is required' : ''
                                                }
                                                required
                                                fullWidth
                                                label="cITCode"
                                                allowNoValue={false}
                                            />
                                        )}
                                        {/*                      button with click event to show all part numbers to choose from?
                            classic drilldown style?       
                            partNumbers */}
                                    </Grid>
                                    <Grid item xs={12}>
                                        <InputField
                                            value={triggerLevel.description}
                                            label="Description"
                                            maxLength={50}
                                            fullWidth
                                            helperText={
                                                descriptionInvalid() ? 'This field is required' : ''
                                            }
                                            required
                                            // rows={2}
                                            onChange={handleResourceFieldChange}
                                            propertyName="description"
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Grid item xs={6}>
                                            <Dropdown
                                                onChange={handleCitChange}
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
                                                label="cITCode"
                                                allowNoValue={false}
                                            />
                                        </Grid>
                                    </Grid>
                                    <Grid item xs={6}>
                                        <InputField
                                            value={triggerLevel.variableTriggerLevel}
                                            label="Variable Trigger Level"
                                            type="number"
                                            maxLength={2}
                                            fullWidth
                                            onChange={handleResourceFieldChange}
                                            propertyName="variableTriggerLevel"
                                        />
                                    </Grid>
                                    <Grid item xs={6}>
                                        <InputField
                                            value={triggerLevel.overrideTriggerLevel}
                                            label="Override Trigger Level"
                                            maxLength={2}
                                            type="number"
                                            fullWidth
                                            onChange={handleResourceFieldChange}
                                            propertyName="overrideTriggerLevel"
                                        />
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
                                            propertyName="workStation"
                                            fullWidth
                                            value={triggerLevel.workStation}
                                            label="Work Station"
                                            allowNoValue
                                            onChange={handleResourceFieldChange}
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
                                            allowNoValue={false}
                                        />
                                    </Grid>
                                    <Grid item xs={10}>
                                        <InputField
                                            value={triggerLevel.story}
                                            label="Story"
                                            fullWidth
                                            onChange={handleResourceFieldChange}
                                            propertyName="story"
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <SaveBackCancelButtons
                                            saveDisabled={viewing() || inputInvalid()}
                                            saveClick={handleSaveClick}
                                            cancelClick={handleCancelClick}
                                            backClick={handleBackClick}
                                        />
                                    </Grid>
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
        routeCode: PropTypes.string,
        description: PropTypes.string,
        notes: PropTypes.string
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
    parts: PropTypes.arrayOf(
        PropTypes.shape({
            partNumber: PropTypes.string
        })
    ).isRequired,
    manufacturingRoutes: PropTypes.arrayOf(
        PropTypes.shape({
            routeCode: PropTypes.string
        })
    ).isRequired,
    cits: PropTypes.arrayOf(
        PropTypes.shape({
            code: PropTypes.string
        })
    ).isRequired,
    employees: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number,
            fullName: PropTypes.string
        })
    ).isRequired,
    getWorkStationsForCit: PropTypes.func.isRequired,
    workStations: PropTypes.arrayOf({
        workStationCode: PropTypes.string,
        description: PropTypes.string
    }).isRequired
};

TriggerLevel.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemErrors: null,
    itemId: null
};

export default TriggerLevel;
