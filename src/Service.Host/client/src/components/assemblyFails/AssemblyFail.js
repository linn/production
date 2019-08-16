import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import DateTimePicker from '@linn-it/linn-form-components-library/cjs/DateTimePicker';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function AssemblyFail({
    editStatus,
    errorMessage,
    item,
    loading,
    snackbarVisible,
    setEditStatus,
    setSnackbarVisible
}) {
    const [assemblyFail, setAssemblyFail] = useState({});
    const [prevAssemblyFail, setPrevAssemblyFail] = useState({});

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevAssemblyFail) {
            setAssemblyFail(item);
            setPrevAssemblyFail(item);
        }
    }, [item, prevAssemblyFail]);

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setAssemblyFail({ ...assemblyFail, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Assembly Fail" />
                    ) : (
                        <Title text="Assembly Fail Details" />
                    )}
                </Grid>
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}
                {loading || !assemblyFail ? (
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
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled={!creating()}
                                value={assemblyFail.id}
                                label="Id"
                                maxLength={10}
                                required
                                onChange={handleFieldChange}
                                propertyName="id"
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.enteredBy}
                                label="Entered By"
                                maxLength={10}
                                onChange={handleFieldChange}
                                propertyName="enteredBy"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.enteredByName}
                                label="Name"
                                maxLength={10}
                                onChange={handleFieldChange}
                                propertyName="enteredByName"
                            />
                        </Grid>
                        <Grid item xs={4} />
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.worksOrderNumber}
                                label="Works Order"
                                maxLength={10}
                                onChange={handleFieldChange}
                                propertyName="worksOrderNumber"
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.numberOfFails}
                                label="Num Fails"
                                maxLength={10}
                                onChange={handleFieldChange}
                                propertyName="numberOfFails"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.partNumber}
                                label="Part Number"
                                maxLength={10}
                                onChange={handleFieldChange}
                                propertyName="partNumber"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.partDescription}
                                label="Description"
                                maxLength={10}
                                onChange={handleFieldChange}
                                propertyName="partDescription"
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.serialNumber}
                                label="Serial Number"
                                maxLength={10}
                                onChange={handleFieldChange}
                                propertyName="serialNumber"
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <DateTimePicker
                                value={assemblyFail.dateTimeFound}
                                label="Found"
                                disabled
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.inSlot}
                                label="In Slot"
                                onChange={handleFieldChange}
                                propertyName="inSlot"
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.machine}
                                label="Machine"
                                onChange={handleFieldChange}
                                propertyName="machine"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                rows={4}
                                value={assemblyFail.reportedFault}
                                label="Fault"
                                onChange={handleFieldChange}
                                propertyName="reportedFault"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                rows={4}
                                value={assemblyFail.analysis}
                                label="Analysis"
                                onChange={handleFieldChange}
                                propertyName="analysis"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                rows={4}
                                value={assemblyFail.engineeringComments}
                                label="Engineering Comments"
                                onChange={handleFieldChange}
                                propertyName="engineeringComments"
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.boardPartNumber}
                                label="Board Part"
                                onChange={handleFieldChange}
                                propertyName="boardPartNumber"
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.boardDescription}
                                label="Description"
                                onChange={handleFieldChange}
                                propertyName="boardDescription"
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.boardSerial}
                                label="Board Serial"
                                onChange={handleFieldChange}
                                propertyName="boardSerial"
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.shift}
                                label="Shift"
                                onChange={handleFieldChange}
                                propertyName="shift"
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.batch}
                                label="Batch"
                                onChange={handleFieldChange}
                                propertyName="boarbatchbatchSerial"
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.aoiEscape}
                                label="AOI Escape"
                                onChange={handleFieldChange}
                                propertyName="aoiEscape"
                            />
                        </Grid>
                        <Grid item xs={6} />
                        <Grid item xs={3}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.circuitRef}
                                label="Circuit Ref"
                                onChange={handleFieldChange}
                                propertyName="circuitRef"
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.circuitPartNumber}
                                label="Circuit Part"
                                onChange={handleFieldChange}
                                propertyName="circuitPartNumber"
                            />
                        </Grid>
                        <Grid item xs={3} />
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.citResponsible}
                                label="CIT Responsible"
                                onChange={handleFieldChange}
                                propertyName="citResponsible"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.citResponsibleName}
                                label="Name"
                                onChange={handleFieldChange}
                                propertyName="citResponsibleName"
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.personResponsible}
                                label="Person Responsible"
                                onChange={handleFieldChange}
                                propertyName="personResponsible"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.personResponsibleName}
                                label="Name"
                                onChange={handleFieldChange}
                                propertyName="personResponsibleName"
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.faultCode}
                                label="Fault Code"
                                onChange={handleFieldChange}
                                propertyName="faultCode"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.faultCodeDescription}
                                label="Description"
                                onChange={handleFieldChange}
                                propertyName="faultCodeDescription"
                            />
                        </Grid>
                        <Grid item xs={6} />
                        <Grid item xs={4}>
                            <DateTimePicker
                                value={assemblyFail.dateTimeComplete}
                                label="Complete"
                                disabled
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.completedBy}
                                label="Completed By"
                                onChange={handleFieldChange}
                                propertyName="completedBy"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.completedByName}
                                label="Name"
                                onChange={handleFieldChange}
                                propertyName="completedByName"
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.returnedBy}
                                label="Returned By"
                                onChange={handleFieldChange}
                                propertyName="returnedBy"
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.returnedBy}
                                label="Returned By"
                                onChange={handleFieldChange}
                                propertyName="returnedBy"
                            />
                        </Grid>
                        <Grid item xs={2}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.returnedByName}
                                label="Name"
                                onChange={handleFieldChange}
                                propertyName="returnedByName"
                            />
                        </Grid>
                        <Grid item xs={4}>
                            <InputField
                                fullWidth
                                disabled
                                rows={4}
                                value={assemblyFail.correctiveAction}
                                label="Corrective Action"
                                onChange={handleFieldChange}
                                propertyName="correctiveAction"
                            />
                        </Grid>
                        <Grid item xs={1}>
                            <InputField
                                fullWidth
                                disabled
                                value={assemblyFail.outSlot}
                                label="Out Slot"
                                onChange={handleFieldChange}
                                propertyName="outSlot"
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <DateTimePicker value={assemblyFail.caDAte} label="CA Date" disabled />
                        </Grid>
                        <Grid item xs={3}>
                            <DateTimePicker
                                value={assemblyFail.dateInvalid}
                                label="Date Invalid"
                                disabled
                            />
                        </Grid>
                    </Fragment>
                )}
            </Grid>
        </Page>
    );
}

AssemblyFail.propTypes = {
    item: PropTypes.shape({
        assemblyFail: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    errorMessage: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

AssemblyFail.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: null,
    errorMessage: ''
};

export default AssemblyFail;
