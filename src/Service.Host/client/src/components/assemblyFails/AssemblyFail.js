import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import DateTimePicker from '@linn-it/linn-form-components-library/cjs/DateTimePicker';
import { makeStyles } from '@material-ui/styles';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    SearchInputField,
    useSearch
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';
import { Button } from '@material-ui/core';
// import WorksOrdersSearch from '../../containers/WorksOrdersSearch';

const useStyles = makeStyles(theme => ({ button: { padding: theme.spacing(6) + '!important' } }));

function AssemblyFail({
    editStatus,
    errorMessage,
    item,
    loading,
    snackbarVisible,
    setEditStatus,
    setSnackbarVisible,
    profile,
    fetchItems,
    clearSearch,
    worksOrders,
    worksOrdersLoading
}) {
    const [searchTerm, setSearchTerm] = useState(null);
    const [assemblyFail, setAssemblyFail] = useState({});
    const [prevAssemblyFail, setPrevAssemblyFail] = useState({});

    const [worksOrder, setWorksOrder] = useState(null);

    const classes = useStyles();

    useSearch(fetchItems, searchTerm, null, 'searchTerm');

    const handleSearchTermChange = (...args) => {
        setSearchTerm(args[1]);
    };

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';

    const worksOrderSearchHelperText = () => {
        if (!worksOrder) {
            return 'Search for a Works Order to get started';
        }
        return 'Search for a different Works Order?';
    };

    useEffect(() => {
        if (item !== prevAssemblyFail) {
            setAssemblyFail(item);
            setPrevAssemblyFail(item);
        }
    }, [item, prevAssemblyFail]);

    useEffect(() => {
        // if (worksOrder && searchTerm === null) {
        //     setSearchTerm(worksOrder.orderNumber);
        // }
        if (creating()) {
            if (worksOrders.length === 1 && !worksOrdersLoading) {
                setWorksOrder(worksOrders[0]);
                setSearchTerm('');
            } else {
                setWorksOrder(null);
            }
        }
    }, [worksOrders, worksOrdersLoading, worksOrder]);

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
                        <Title text="Log An Assembly Fail" />
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
                        {!creating() ? (
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={assemblyFail.id}
                                    label="Id"
                                    maxLength={10}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="id"
                                />
                            </Grid>
                        ) : (
                            <Fragment />
                        )}

                        {creating() ? (
                            <Fragment>
                                <Grid item xs={6}>
                                    <SearchInputField
                                        label={worksOrderSearchHelperText()}
                                        fullWidth
                                        placeholder="Order Number"
                                        onChange={handleSearchTermChange}
                                        propertyName="searchTerm"
                                        type="number"
                                        value={searchTerm}
                                    />{' '}
                                </Grid>
                                {/* <Grid item xs={2}>
                                    <Button
                                        onClick={() => {
                                            clearSearch();
                                            setSearchTerm(null);
                                            setWorksOrder(null);
                                        }}
                                    >
                                        {' '}
                                        CLEAR{' '}
                                    </Button>
                                </Grid> */}
                                <Grid item xs={6} />
                            </Fragment>
                        ) : (
                            <Fragment />
                        )}

                        {worksOrder || !creating() ? (
                            <Fragment>
                                <Grid item xs={2}>
                                    <InputField
                                        disabled
                                        value={
                                            creating()
                                                ? worksOrder
                                                    ? worksOrder.orderNumber
                                                    : null
                                                : assemblyFail.worksOrderNumber
                                        }
                                        type="number"
                                        label="Works Order"
                                        maxLength={8}
                                        onChange={handleFieldChange}
                                        propertyName="worksOrderNumber"
                                    />{' '}
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={
                                            creating()
                                                ? worksOrder.partNumber
                                                : assemblyFail.partNumber
                                        }
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
                                        rows={3}
                                        value={
                                            creating()
                                                ? worksOrder.partDescription
                                                : assemblyFail.partDescription
                                        }
                                        label="Description"
                                        maxLength={10}
                                        onChange={handleFieldChange}
                                        propertyName="partDescription"
                                    />
                                </Grid>
                                {creating() && <Grid item xs={2} />}

                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={
                                            creating()
                                                ? profile.employee.replace('/employees/', '')
                                                : assemblyFail.enteredBy
                                        }
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
                                        value={
                                            creating() ? profile.name : assemblyFail.enteredByName
                                        }
                                        label="Name"
                                        maxLength={10}
                                        onChange={handleFieldChange}
                                        propertyName="enteredByName"
                                    />
                                </Grid>
                                <Grid item xs={6} />
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={assemblyFail.serialNumber}
                                        label="Serial Number"
                                        type="number"
                                        maxLength={10}
                                        onChange={handleFieldChange}
                                        propertyName="serialNumber"
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={assemblyFail.numberOfFails}
                                        label="Num Fails"
                                        maxLength={10}
                                        type="number"
                                        onChange={handleFieldChange}
                                        propertyName="numberOfFails"
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
                                        value={assemblyFail.inSlot}
                                        label="In Slot"
                                        onChange={handleFieldChange}
                                        propertyName="inSlot"
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={assemblyFail.machine}
                                        label="Machine"
                                        onChange={handleFieldChange}
                                        propertyName="machine"
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
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
                                    <DateTimePicker
                                        value={assemblyFail.caDAte}
                                        label="CA Date"
                                        disabled
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <DateTimePicker
                                        value={assemblyFail.dateInvalid}
                                        label="Date Invalid"
                                        disabled
                                    />
                                </Grid>
                            </Fragment>
                        ) : (
                            <Fragment />
                        )}
                        {worksOrdersLoading && (
                            <Grid item xs={12}>
                                <Loading />
                            </Grid>
                        )}
                        {!worksOrdersLoading && worksOrders && worksOrders.length > 1 && (
                            <Typography>
                                Refine your search.. more than one Works Order returned.
                            </Typography>
                        )}
                        {!worksOrdersLoading &&
                            searchTerm &&
                            worksOrders &&
                            worksOrders.length === 0 && (
                                <Typography>No results to match the search criteria.</Typography>
                            )}
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
    profile: PropTypes.shape({}),
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
    errorMessage: '',
    profile: { employee: '', name: '' }
};

export default AssemblyFail;
