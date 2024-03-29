﻿import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    utilities
} from '@linn-it/linn-form-components-library';
import Button from '@material-ui/core/Button';
import Page from '../../containers/Page';

function PtlSettings({
    editStatus,
    itemError,
    history,
    item,
    loading,
    snackbarVisible,
    updateItem,
    setEditStatus,
    setSnackbarVisible,
    setStartTriggerRunMessageVisible,
    startTriggerRun,
    startTriggerRunMessageVisible,
    startTriggerRunMessageText
}) {
    const [ptlSettings, setPtlSettings] = useState({});
    const [prevPtlSettings, setPrevPtlSettings] = useState({});

    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevPtlSettings) {
            setPtlSettings(item);
            setPrevPtlSettings(item);
        }
    }, [item, prevPtlSettings]);

    const editingAllowed = utilities.getHref(item, 'edit') !== null;
    const startTriggerRunAllowed = utilities.getHref(item, 'start-trigger-run') !== null;

    const handleSaveClick = () => {
        if (editing()) {
            updateItem('', ptlSettings);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setPtlSettings(item);
        setEditStatus('view');
    };

    const handleTriggerRunButtonClick = () => {
        startTriggerRun(null);
    };

    const handleBackClick = () => {
        history.push('/production/maintenance');
    };

    const handleNumberFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }

        setPtlSettings({ ...ptlSettings, [propertyName]: Number(newValue) });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Production Trigger Level Settings" />
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError?.statusText} />
                    </Grid>
                )}
                {loading || !ptlSettings ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    <>
                        <SnackbarMessage
                            visible={snackbarVisible}
                            onClose={() => setSnackbarVisible(false)}
                            message="Save Successful"
                        />
                        <SnackbarMessage
                            visible={startTriggerRunMessageVisible}
                            onClose={() => setStartTriggerRunMessageVisible(false)}
                            message={startTriggerRunMessageText}
                        />
                        <Grid item xs={4}>
                            <InputField
                                disabled={!editingAllowed}
                                value={ptlSettings.buildToMonthEndFromDays}
                                label="Build To Month End From Days"
                                required
                                type="number"
                                onChange={handleNumberFieldChange}
                                propertyName="buildToMonthEndFromDays"
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                disabled={!editingAllowed}
                                value={ptlSettings.finalAssemblyDaysToLookAhead}
                                label="Final Assembly Days To Look Ahead"
                                type="number"
                                required
                                onChange={handleNumberFieldChange}
                                propertyName="finalAssemblyDaysToLookAhead"
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                disabled={!editingAllowed}
                                value={ptlSettings.subAssemblyDaysToLookAhead}
                                label="Sub-assembly Days To Look Ahead"
                                type="number"
                                required
                                onChange={handleNumberFieldChange}
                                propertyName="subAssemblyDaysToLookAhead"
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={7}>
                            <SaveBackCancelButtons
                                saveDisabled={viewing()}
                                saveClick={handleSaveClick}
                                cancelClick={handleCancelClick}
                                backClick={handleBackClick}
                            />
                        </Grid>
                        <Grid item xs={5} />
                        {startTriggerRunAllowed ? (
                            <>
                                <Grid item xs={12}>
                                    <div>
                                        Trigger runs start regularly throughout the day. Please do
                                        not start a new one unless you are sure.
                                    </div>
                                </Grid>
                                <Grid item xs={3}>
                                    <Button
                                        onClick={handleTriggerRunButtonClick}
                                        variant="outlined"
                                        color="secondary"
                                    >
                                        Start Trigger Run
                                    </Button>
                                </Grid>
                                <Grid item xs={9} />
                            </>
                        ) : (
                            ''
                        )}
                    </>
                )}
            </Grid>
        </Page>
    );
}

PtlSettings.propTypes = {
    item: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({ statusText: PropTypes.string }),
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    setStartTriggerRunMessageVisible: PropTypes.func.isRequired,
    startTriggerRun: PropTypes.func.isRequired,
    startTriggerRunMessageVisible: PropTypes.bool,
    startTriggerRunMessageText: PropTypes.string
};

PtlSettings.defaultProps = {
    item: {},
    snackbarVisible: false,
    updateItem: null,
    loading: null,
    itemError: null,
    startTriggerRunMessageVisible: false,
    startTriggerRunMessageText: ''
};

export default PtlSettings;
