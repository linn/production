import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import DateTimePicker from '@linn-it/linn-form-components-library/cjs/DateTimePicker';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    SearchInputField,
    useSearch,
    Dropdown,
    SaveBackCancelButtons,
    DatePicker
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
    faultCodes,
    addItem,
    updateItem,
    itemId,
    history
}) {
    // render constants
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';
    const getInitialState = () => {
        if (creating()) {
            return {
                numberOfFails: 1,
                dateTimeFound: new Date().toISOString(),
                dateTimeComplete: null,
                dateInvalid: null,
                caDate: null
            };
        }
        return {
            numberOfFails: 1,
            dateTimeFound: new Date().toISOString(),
            dateTimeComplete: null,
            dateInvalid: null,
            caDate: null
        };
    };

    // state
    const [searchTerm, setSearchTerm] = useState(null);
    const [assemblyFail, setAssemblyFail] = useState(getInitialState());
    const [prevAssemblyFail, setPrevAssemblyFail] = useState({});
    const [dateTimeComplete, setDateTimeComplete] = useState(null);
    const [caDate, setCaDate] = useState(null);
    const [dateInvalid, setDateInvalid] = useState(null);

    useSearch(fetchItems, searchTerm, null, 'searchTerm');

    const inputInvalid = () => !assemblyFail.worksOrderNumber;
    const notCompleted = () => !assemblyFail.dateTimeComplete || creating();

    const worksOrderSearchHelperText = () => {
        if (assemblyFail.worksOrderNumber) {
            return 'Change works order?';
        }
        return 'Search for a Works Order to get started';
    };

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, assemblyFail);
            setEditStatus('view');
        } else if (creating()) {
            addItem(assemblyFail);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        if (creating()) {
            setAssemblyFail({
                enteredBy: profile.employee.replace('/employees/', ''),
                enteredByName: profile.name
            });
        } else {
            setAssemblyFail(item);
        }
        setEditStatus('view');
        setSearchTerm('');
    };

    const handleBackClick = () => {
        history.push('/production/quality/ate/fault-codes/');
    };

    //  effects
    useEffect(() => {
        if (editStatus !== 'create' && item !== prevAssemblyFail) {
            setAssemblyFail(item);
            setPrevAssemblyFail(item);
        }
    }, [item, prevAssemblyFail, editStatus]);

    useEffect(() => {
        setAssemblyFail(a => ({ ...a, dateTimeComplete }));
    }, [dateTimeComplete, setDateTimeComplete]);

    useEffect(() => {
        setAssemblyFail(a => ({ ...a, dateInvalid }));
    }, [dateInvalid, setDateInvalid]);

    useEffect(() => {
        setAssemblyFail(a => ({ ...a, caDate }));
    }, [caDate, setCaDate]);

    useEffect(() => {
        if (editStatus === 'create' && profile) {
            setAssemblyFail(a => ({
                ...a,
                enteredBy: profile.employee.replace('/employees/', ''),
                enteredByName: profile.name
            }));
        }
    }, [profile, editStatus]);

    useEffect(() => {
        if (assemblyFail.boardPart) {
            fetchPcasRevisionsForBoardPart('searchTerm', assemblyFail.boardPart);
        }
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
        if (employees && assemblyFail.personResponsible) {
            setAssemblyFail(a => ({
                ...a,
                personResponsibleName: employees.find(
                    e => Number(e.id) === Number(a.personResponsible).fullName
                )
            }));
        }
    }, [employees, assemblyFail.personResponsible]);

    useEffect(() => {
        if (employees && assemblyFail.returnedBy) {
            setAssemblyFail(a => ({
                ...a,
                returnedByName: employees.find(e => Number(e.id) === Number(a.returnedBy).fullName)
            }));
        }
    }, [employees, assemblyFail.returnedBy]);

    useEffect(() => {
        if (employees && assemblyFail.completedBy) {
            setAssemblyFail(a => ({
                ...a,
                completedByName: employees.find(
                    e => Number(e.id) === Number(a.completedBy).fullName
                )
            }));
        }
    }, [employees, assemblyFail.completedBy]);

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
                setAssemblyFail(a => ({
                    ...a,
                    worksOrderNumber: worksOrders[0].orderNumber,
                    partNumber: worksOrders[0].partNumber,
                    partDescription: worksOrders[0].partDescription
                }));
            } else {
                setAssemblyFail(a => ({
                    ...a,
                    boardPart: null,
                    worksOrderNumber: null,
                    partNumber: null,
                    partDescription: null
                }));
            }
        }
    }, [worksOrders, worksOrdersLoading, editStatus]);

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

                        {assemblyFail.worksOrderNumber || !creating() ? (
                            <Fragment>
                                <Grid item xs={2}>
                                    <InputField
                                        disabled
                                        value={assemblyFail.worksOrderNumber}
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
                                        rows={3}
                                        value={assemblyFail.partDescription}
                                        label="Description"
                                        maxLength={10}
                                        onChange={handleFieldChange}
                                        propertyName="partDescription"
                                    />
                                </Grid>
                                {creating() && <Grid item xs={2} />}

                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={assemblyFail.enteredByName}
                                        label="Entered By"
                                        maxLength={10}
                                        onChange={handleFieldChange}
                                        propertyName="enteredByName"
                                    />
                                </Grid>
                                <Grid item xs={6} />
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        disabled={!notCompleted() || viewing()}
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
                                        disabled={!notCompleted() || viewing()}
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
                                        disabled={!notCompleted() || viewing()}
                                        label="In Slot"
                                        onChange={handleFieldChange}
                                        propertyName="inSlot"
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <InputField
                                        fullWidth
                                        value={assemblyFail.machine}
                                        disabled={!notCompleted() || viewing()}
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
                                        disabled={!notCompleted() || viewing()}
                                        label="Fault"
                                        onChange={handleFieldChange}
                                        propertyName="reportedFault"
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        disabled={!notCompleted() || viewing()}
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
                                        disabled={viewing()}
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
                                        value={assemblyFail.aoiEscape}
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
                                                ? [''].concat(
                                                      pcasRevisions.map(r => ({
                                                          id: r.cref,
                                                          displayText: `${r.cref}  -  ${r.partNumber}`
                                                      }))
                                                  )
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
                                        items={
                                            cits
                                                ? [''].concat(
                                                      Array.from(
                                                          new Set(
                                                              cits.map(c => ({
                                                                  id: c.code,
                                                                  displayText: `${c.code} - ${c.name}`
                                                              }))
                                                          )
                                                      )
                                                  )
                                                : ['']
                                        }
                                        fullWidth
                                        value={assemblyFail.citResponsible}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <Dropdown
                                        label="Person Responsible"
                                        type="number"
                                        propertyName="personResponsible"
                                        disabled={!notCompleted() || viewing()}
                                        items={
                                            employees
                                                ? [''].concat(
                                                      Array.from(
                                                          new Set(
                                                              employees.map(c => ({
                                                                  id: c.id,
                                                                  displayText: c.fullName
                                                              }))
                                                          )
                                                      )
                                                  )
                                                : ['']
                                        }
                                        fullWidth
                                        value={assemblyFail.personResponsible}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <Dropdown
                                        label="Fault Code"
                                        propertyName="faultCode"
                                        disabled={!notCompleted() || viewing()}
                                        items={
                                            faultCodes
                                                ? [''].concat(
                                                      faultCodes.map(c => ({
                                                          id: c.faultCode,
                                                          displayText: `${c.faultCode} - ${c.description}`
                                                      }))
                                                  )
                                                : ['']
                                        }
                                        fullWidth
                                        value={assemblyFail.faultCode}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={6} />
                                <Grid item xs={4}>
                                    <DateTimePicker
                                        value={dateTimeComplete}
                                        label="Complete"
                                        disabled={!notCompleted() || viewing()}
                                        onChange={setDateTimeComplete}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <Dropdown
                                        label="Completed By"
                                        propertyName="completedBy"
                                        type="number"
                                        disabled={!notCompleted() || viewing()}
                                        items={
                                            employees
                                                ? [''].concat(
                                                      employees.map(c => ({
                                                          id: c.id,
                                                          displayText: c.fullName
                                                      }))
                                                  )
                                                : ['']
                                        }
                                        fullWidth
                                        value={assemblyFail.completedBy}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>

                                <Grid item xs={2}>
                                    <Dropdown
                                        label="Returned By"
                                        type="number"
                                        propertyName="returnedBy"
                                        disabled={!notCompleted() || viewing()}
                                        items={
                                            employees
                                                ? [''].concat(
                                                      employees.map(c => ({
                                                          id: c.id,
                                                          displayText: c.fullName
                                                      }))
                                                  )
                                                : ['']
                                        }
                                        fullWidth
                                        value={assemblyFail.returnedBy}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        rows={4}
                                        disabled={viewing()}
                                        value={assemblyFail.correctiveAction}
                                        label="Corrective Action"
                                        onChange={handleFieldChange}
                                        propertyName="correctiveAction"
                                    />
                                </Grid>
                                <Grid item xs={1}>
                                    <InputField
                                        fullWidth
                                        disabled={!notCompleted() || viewing()}
                                        value={assemblyFail.outSlot}
                                        label="Out Slot"
                                        onChange={handleFieldChange}
                                        propertyName="outSlot"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <DatePicker
                                        disabled={viewing()}
                                        value={assemblyFail.caDate}
                                        label="CA Date"
                                        onChange={setCaDate}
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <DatePicker
                                        disabled={viewing()}
                                        value={assemblyFail.dateInvalid}
                                        onChange={setDateInvalid}
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
                <Grid item xs={12}>
                    <SaveBackCancelButtons
                        saveDisabled={inputInvalid() || viewing()}
                        saveClick={handleSaveClick}
                        cancelClick={handleCancelClick}
                        backClick={handleBackClick}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

AssemblyFail.propTypes = {
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
    snackbarVisible: false,
    loading: null,
    errorMessage: '',
    profile: { employee: '', name: '' },
    boardParts: []
};

export default AssemblyFail;
