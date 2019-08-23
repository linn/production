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
    useSearch,
    Dropdown
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

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
    worksOrdersLoading,
    boardParts,
    boardPartsLoading,
    pcasRevisions,
    fetchPcasRevisionsForBoardPart,
    cits,
    employees,
    faultCodes
}) {
    // state
    const [searchTerm, setSearchTerm] = useState(null);
    const [assemblyFail, setAssemblyFail] = useState({ numberOfFails: 1 });
    const [prevAssemblyFail, setPrevAssemblyFail] = useState({});
    const [worksOrder, setWorksOrder] = useState(null);

    useSearch(fetchItems, searchTerm, null, 'searchTerm');

    // render constants
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const notCompleted = () => !assemblyFail.dateTimeComplete || creating();

    const worksOrderSearchHelperText = () => {
        if (worksOrder || assemblyFail.worksOrderNumber) {
            return 'Change works order?';
        }
        return 'Search for a Works Order to get started';
    };

    //  effects
    useEffect(() => {
        if (editStatus !== 'create' && item !== prevAssemblyFail) {
            setAssemblyFail(item);
            setPrevAssemblyFail(item);
        }
    }, [item, prevAssemblyFail, editStatus]);

    useEffect(() => {
        fetchPcasRevisionsForBoardPart('searchTerm', assemblyFail.boardPart);
    }, [assemblyFail.boardPart, fetchPcasRevisionsForBoardPart]);

    useEffect(() => {
        if (assemblyFail.boardPart) {
            setAssemblyFail(a => ({
                ...a,
                boardDescription: boardParts.find(p => p.partNumber === a.boardPart).description
            }));
        }
    }, [assemblyFail.boardPart, boardParts]);

    useEffect(() => {
        if (pcasRevisions && assemblyFail.circuitRef) {
            setAssemblyFail(a => ({
                ...a,
                circuitPartNumber: pcasRevisions.find(p => p.cref === a.circuitRef).partNumber
            }));
        }
    }, [pcasRevisions, assemblyFail.circuitRef]);

    useEffect(() => {
        if (cits && assemblyFail.citResponsible) {
            setAssemblyFail(a => ({
                ...a,
                citResponsibleName: cits.find(c => c.code === a.citResponsible).name
            }));
        }
    }, [cits, assemblyFail.citResponsible]);

    useEffect(() => {
        if (faultCodes && assemblyFail.faultCode) {
            setAssemblyFail(a => ({
                ...a,
                faultCodeDescription: faultCodes.find(c => c.faultCode === a.faultCode).description
            }));
        }
    }, [faultCodes, assemblyFail.faultCode]);

    useEffect(() => {
        if (editStatus === 'create') {
            if (worksOrders.length === 1 && !worksOrdersLoading) {
                setWorksOrder(worksOrders[0]);
            } else {
                setWorksOrder(null);
                setAssemblyFail(a => ({ ...a, boardPart: null }));
            }
        }
    }, [worksOrders, worksOrdersLoading, worksOrder, editStatus]);

    // form field change handler
    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setAssemblyFail({ ...assemblyFail, [propertyName]: newValue });
    };

    // works orders search field change hanlder
    const handleSearchTermChange = (...args) => {
        setSearchTerm(args[1]);
    };

    // Dropdown item lists
    const getBoardPartItems = () => {
        if (creating()) {
            return [''].concat(boardParts.map(p => p.partNumber));
        }
        if (assemblyFail && assemblyFail.boardPartNumber) {
            return [assemblyFail.boardPartNumber];
        }
        return [''];
    };

    const aoiEscapeValues = ['', 'Y', 'N'];

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
                            <Fragment>
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
                                <Grid item xs={10} />{' '}
                            </Fragment>
                        ) : (
                            <Fragment />
                        )}

                        {creating() || notCompleted() ? (
                            <Fragment>
                                <Grid item xs={4}>
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
                                <Grid item xs={6} />
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
                                        disabled={!notCompleted()}
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
                                        disabled={!notCompleted()}
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
                                        disabled={!notCompleted()}
                                        label="In Slot"
                                        onChange={handleFieldChange}
                                        propertyName="inSlot"
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={assemblyFail.machine}
                                        disabled={!notCompleted()}
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
                                        disabled={!notCompleted()}
                                        label="Fault"
                                        onChange={handleFieldChange}
                                        propertyName="reportedFault"
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        disabled={!notCompleted()}
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
                                        rows={4}
                                        value={assemblyFail.engineeringComments}
                                        label="Engineering Comments"
                                        onChange={handleFieldChange}
                                        propertyName="engineeringComments"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <Dropdown
                                        label="Board Part"
                                        propertyName="boardPart"
                                        disabled={!notCompleted()}
                                        items={getBoardPartItems()}
                                        fullWidth
                                        value={assemblyFail.boardPart}
                                        onChange={handleFieldChange}
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
                                        disabled={!notCompleted()}
                                        value={assemblyFail.boardSerial}
                                        label="Board Serial"
                                        onChange={handleFieldChange}
                                        propertyName="boardSerial"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        fullWidth
                                        disabled={!notCompleted()}
                                        value={assemblyFail.shift}
                                        label="Shift"
                                        onChange={handleFieldChange}
                                        propertyName="shift"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        fullWidth
                                        disabled={!notCompleted()}
                                        value={assemblyFail.batch}
                                        label="Batch"
                                        onChange={handleFieldChange}
                                        propertyName="batch"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <Dropdown
                                        label="AOI Escape"
                                        propertyName="aoiEscape"
                                        disabled={!notCompleted()}
                                        items={aoiEscapeValues}
                                        fullWidth
                                        value={assemblyFail.boardPart}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={6} />
                                <Grid item xs={3}>
                                    <Dropdown
                                        label="Circuit Ref"
                                        propertyName="circuitRef"
                                        disabled={
                                            (pcasRevisions && pcasRevisions.length === 0) ||
                                            !notCompleted()
                                        }
                                        items={
                                            pcasRevisions
                                                ? [''].concat(pcasRevisions.map(r => r.cref))
                                                : ['']
                                        }
                                        fullWidth
                                        value={assemblyFail.circuitRef}
                                        onChange={handleFieldChange}
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
                                    <Dropdown
                                        label="CIT Responsible"
                                        propertyName="citResponsible"
                                        disabled={!notCompleted()}
                                        items={cits ? [''].concat(cits.map(c => c.code)) : ['']}
                                        fullWidth
                                        value={assemblyFail.citResponsible}
                                        onChange={handleFieldChange}
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
                                    {/*  // TODO - decide what to do with employee numb */}
                                    <InputField
                                        fullWidth
                                        disabled={!notCompleted()}
                                        value={assemblyFail.personResponsible}
                                        label="User Number"
                                        onChange={handleFieldChange}
                                        propertyName="personResponsible"
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <Dropdown
                                        label="Person Responsible"
                                        propertyName="personResponsibleName"
                                        disabled={!notCompleted()}
                                        items={
                                            employees
                                                ? [''].concat(
                                                      employees
                                                          .filter(e => e.fullName)
                                                          .map(c => c.fullName)
                                                  )
                                                : ['']
                                        }
                                        fullWidth
                                        value={assemblyFail.personResponsibleName}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <Dropdown
                                        label="Fault Code"
                                        propertyName="faultCode"
                                        disabled={!notCompleted()}
                                        items={
                                            faultCodes
                                                ? [''].concat(faultCodes.map(c => c.faultCode))
                                                : ['']
                                        }
                                        fullWidth
                                        value={assemblyFail.faultCode}
                                        onChange={handleFieldChange}
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
                                        disabled={!notCompleted()}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    {/*  // TODO - dropdown */}
                                    <InputField
                                        fullWidth
                                        disabled={!notCompleted()}
                                        value={assemblyFail.completedBy}
                                        label="Completed By"
                                        onChange={handleFieldChange}
                                        propertyName="completedBy"
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    {/*  // TODO - autoset */}
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
                                    {/*  // TODO - dropdown */}
                                    <InputField
                                        fullWidth
                                        disabled={!notCompleted()}
                                        value={assemblyFail.returnedBy}
                                        label="Returned By"
                                        onChange={handleFieldChange}
                                        propertyName="returnedBy"
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    {/*  // TODO - autocomplete */}
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
                                        disabled={!notCompleted()}
                                        value={assemblyFail.outSlot}
                                        label="Out Slot"
                                        onChange={handleFieldChange}
                                        propertyName="outSlot"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <DateTimePicker value={assemblyFail.caDate} label="CA Date" />
                                </Grid>
                                <Grid item xs={3}>
                                    <DateTimePicker
                                        value={assemblyFail.dateInvalid}
                                        label="Date Invalid"
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
    setSnackbarVisible: PropTypes.func.isRequired,
    boardParts: PropTypes.arrayOf(PropTypes.shape({}))
};

AssemblyFail.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: null,
    errorMessage: '',
    profile: { employee: '', name: '' },
    boardParts: []
};

export default AssemblyFail;
