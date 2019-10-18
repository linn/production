import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    SaveBackCancelButtons,
    DatePicker,
    DateTimePicker,
    ValidatedInputDialog,
    Dropdown
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';

function AssemblyFail({
    editStatus,
    itemErrors,
    item,
    loading,
    snackbarVisible,
    setEditStatus,
    setSnackbarVisible,
    profile,
    searchWorksOrders,
    worksOrdersSearchResults,
    worksOrdersSearchLoading,
    clearWorksOrdersSearch,
    boardParts,
    pcasRevisions,
    fetchPcasRevisionsForBoardPart,
    cits,
    smtShifts,
    employees,
    faultCodes,
    addItem,
    updateItem,
    itemId,
    history
}) {
    const [assemblyFail, setAssemblyFail] = useState({
        numberOfFails: 1,
        dateTimeFound: new Date().toISOString(),
        dateTimeComplete: null,
        dateInvalid: null,
        caDate: null,
        boardPartNumber: ''
    });
    const [prevAssemblyFail, setPrevAssemblyFail] = useState({});

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';
    const aoiEscapeValues = ['', 'Y', 'N'];
    const inputInvalid = () => !assemblyFail.worksOrderNumber;
    const completed = () => !!item?.dateTimeComplete;

    useEffect(() => {
        if (editStatus !== 'create' && item && item !== prevAssemblyFail) {
            setAssemblyFail(item);
            setPrevAssemblyFail(item);
        }
    }, [item, prevAssemblyFail, editStatus]);

    useEffect(() => {
        clearWorksOrdersSearch();
    }, [clearWorksOrdersSearch]);

    useEffect(() => {
        if (editStatus === 'create' && profile) {
            setAssemblyFail(a => ({
                ...a,
                enteredBy: profile.employee.replace('/employees/', ''), // the current user
                enteredByName: profile.name
            }));
        }
    }, [profile, editStatus]);

    useEffect(() => {
        setAssemblyFail(a => ({
            ...a,
            circuitRef: '',
            circuitPartNumber: ''
        }));
        fetchPcasRevisionsForBoardPart(
            'searchTerm',
            assemblyFail.boardPartNumber ? assemblyFail.boardPartNumber : null
        );
    }, [assemblyFail.boardPartNumber, fetchPcasRevisionsForBoardPart]);

    useEffect(() => {
        if (assemblyFail.boardPartNumber) {
            setAssemblyFail(a => ({
                ...a,
                boardDescription: boardParts.find(p => p.partNumber === a.boardPartNumber)
                    ?.description
            }));
        }
    }, [assemblyFail.boardPartNumber, boardParts]);

    useEffect(() => {
        if (assemblyFail.circuitRef) {
            setAssemblyFail(a => ({
                ...a,
                circuitPartNumber: pcasRevisions?.find(p => p.cref === a.circuitRef)?.partNumber
            }));
        }
    }, [pcasRevisions, assemblyFail.circuitRef]);

    useEffect(() => {
        if (cits && assemblyFail.citResponsible) {
            setAssemblyFail(a => ({
                ...a,
                citResponsibleName: cits.find(c => c.code === a.citResponsible)?.name
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
                faultCodeDescription: faultCodes.find(c => c.faultCode === a.faultCode)?.description
            }));
        }
    }, [faultCodes, assemblyFail.faultCode]);

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setAssemblyFail({ ...assemblyFail, [propertyName]: newValue });
    };

    const handleSaveClick = () => {
        if (editing()) {
            console.log(JSON.stringify(assemblyFail));
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
        clearWorksOrdersSearch('');
    };

    const handleBackClick = () => {
        history.push('/production/quality/assembly-fails');
    };

    const useStyles = makeStyles(theme => ({
        marginTop: {
            marginTop: theme.spacing(2),
            marginLeft: theme.spacing(-3)
        }
    }));
    const classes = useStyles();

    return (
        <Page showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Log An Assembly Fail" />
                    ) : (
                        <Title text="Assembly Fail Details" />
                    )}
                </Grid>
                {itemErrors &&
                    !loading &&
                    itemErrors?.map(itemError => (
                        <Grid item xs={12}>
                            <ErrorCard errorMessage={`${itemError.item} ${itemError.statusText}`} />
                        </Grid>
                    ))}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    assemblyFail.id !== '' &&
                    !itemErrors?.some(e => e.status === 404) && ( // don't render this form if things 404
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
                                </Fragment>
                            ) : (
                                <Fragment />
                            )}

                            <Fragment>
                                <Grid item xs={6}>
                                    <InputField
                                        label="Works Order"
                                        maxLength={14}
                                        fullWidth
                                        value={assemblyFail.worksOrderNumber}
                                        disabled
                                        propertyName="worksOrder"
                                    />
                                </Grid>
                                {creating() && (
                                    <Grid item xs={2}>
                                        <div className={classes.marginTop}>
                                            <ValidatedInputDialog
                                                title="Enter a Valid Works Order"
                                                searchItems={worksOrdersSearchResults}
                                                loading={worksOrdersSearchLoading}
                                                onAccept={accepted => {
                                                    setEditStatus('edit');
                                                    setAssemblyFail(a => ({
                                                        ...a,
                                                        worksOrderNumber: accepted.orderNumber,
                                                        partNumber: accepted.partNumber,
                                                        partDescription: accepted.partDescription
                                                    }));
                                                }}
                                                fetchItems={searchWorksOrders}
                                                clearSearch={clearWorksOrdersSearch}
                                            />
                                        </div>
                                    </Grid>
                                )}
                            </Fragment>
                            <Grid item xs={2} />
                            {assemblyFail.worksOrderNumber || !creating() ? (
                                <Fragment>
                                    <Grid item xs={3}>
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
                                    <Grid item xs={3}>
                                        <DateTimePicker
                                            value={assemblyFail.dateTimeFound}
                                            label="Found"
                                            disabled
                                        />
                                    </Grid>
                                    <Grid item xs={6} />
                                    <Grid item xs={3}>
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
                                    <Grid item xs={5} />
                                    <Grid item xs={2}>
                                        <InputField
                                            fullWidth
                                            disabled={completed()}
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
                                            value={assemblyFail.inSlot}
                                            disabled={completed()}
                                            label="In Slot"
                                            onChange={handleFieldChange}
                                            propertyName="inSlot"
                                        />
                                    </Grid>
                                    <Grid item xs={2}>
                                        <InputField
                                            fullWidth
                                            value={assemblyFail.machine}
                                            disabled={completed()}
                                            label="Machine"
                                            onChange={handleFieldChange}
                                            propertyName="machine"
                                        />
                                    </Grid>
                                    <Grid item xs={2}>
                                        <InputField
                                            fullWidth
                                            disabled={completed()}
                                            value={assemblyFail.numberOfFails}
                                            label="Num Fails"
                                            maxLength={10}
                                            type="number"
                                            onChange={handleFieldChange}
                                            propertyName="numberOfFails"
                                        />
                                    </Grid>
                                    <Grid item xs={4} />
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            rows={4}
                                            value={assemblyFail.reportedFault}
                                            disabled={completed()}
                                            label="Fault"
                                            onChange={handleFieldChange}
                                            propertyName="reportedFault"
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled={completed()}
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
                                    <Grid item xs={2}>
                                        <Dropdown
                                            label="Fault Code"
                                            propertyName="faultCode"
                                            disabled={completed()}
                                            items={
                                                faultCodes.length > 0
                                                    ? [{ id: '', displayText: '' }].concat(
                                                          faultCodes.map(c => ({
                                                              id: c.faultCode,
                                                              displayText: `${c.faultCode} - ${c.description}`
                                                          }))
                                                      )
                                                    : ['']
                                            }
                                            fullWidth
                                            value={assemblyFail.faultCode?.toString()}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Dropdown
                                            label="Shift"
                                            propertyName="shift"
                                            disabled={smtShifts?.length === 0 || completed()}
                                            items={
                                                smtShifts
                                                    ? [''].concat(
                                                          smtShifts.map(s => ({
                                                              id: s.shift,
                                                              displayText: `${s.shift}  -  ${s.description}`
                                                          }))
                                                      )
                                                    : ['']
                                            }
                                            fullWidth
                                            value={assemblyFail.shift}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
                                            disabled={completed()}
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
                                            disabled={completed()}
                                            items={aoiEscapeValues}
                                            fullWidth
                                            value={assemblyFail.aoiEscape?.toString()}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Dropdown
                                            label="Board Part"
                                            propertyName="boardPartNumber"
                                            disabled={completed()}
                                            items={[''].concat(boardParts?.map(p => p.partNumber))}
                                            fullWidth
                                            value={assemblyFail.boardPartNumber?.toString()}
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
                                        <Dropdown
                                            label="Circuit Ref"
                                            propertyName="circuitRef"
                                            disabled={
                                                (pcasRevisions && pcasRevisions.length === 0) ||
                                                completed()
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
                                            value={assemblyFail.circuitRef?.toString()}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
                                            disabled={completed()}
                                            value={assemblyFail.boardSerial}
                                            label="Board Serial"
                                            onChange={handleFieldChange}
                                            propertyName="boardSerial"
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Dropdown
                                            label="CIT Responsible"
                                            propertyName="citResponsible"
                                            disabled={completed()}
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
                                            value={assemblyFail.citResponsible?.toString()}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Dropdown
                                            label="Person Responsible"
                                            type="number"
                                            propertyName="personResponsible"
                                            disabled={completed()}
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
                                            value={assemblyFail.personResponsible?.toString()}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={1} />
                                    <Grid item xs={4}>
                                        <DateTimePicker
                                            value={assemblyFail?.dateTimeComplete}
                                            label="Complete"
                                            disabled={completed()}
                                            onChange={value => {
                                                setEditStatus('edit');
                                                setAssemblyFail(a => ({
                                                    ...a,
                                                    dateTimeComplete: value
                                                }));
                                            }}
                                        />
                                    </Grid>
                                    <Grid item xs={2}>
                                        <Dropdown
                                            label="Completed By"
                                            propertyName="completedBy"
                                            type="number"
                                            disabled={completed()}
                                            items={
                                                employees
                                                    ? [''].concat(
                                                          employees
                                                              .filter(c => !!c.fullName)
                                                              .map(c => ({
                                                                  id: c.id,
                                                                  displayText: c.fullName
                                                              }))
                                                      )
                                                    : ['']
                                            }
                                            fullWidth
                                            value={assemblyFail.completedBy?.toString()}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={2}>
                                        <InputField
                                            fullWidth
                                            disabled={completed()}
                                            value={assemblyFail.outSlot}
                                            label="Out Slot"
                                            onChange={handleFieldChange}
                                            propertyName="outSlot"
                                        />
                                    </Grid>
                                    <Grid item xs={4} />
                                    <Grid item xs={2}>
                                        <Dropdown
                                            label="Returned By"
                                            type="number"
                                            propertyName="returnedBy"
                                            disabled={completed()}
                                            items={
                                                employees
                                                    ? [''].concat(
                                                          employees
                                                              .filter(c => !!c.fullName)
                                                              .map(c => ({
                                                                  id: c.id,
                                                                  displayText: c.fullName
                                                              }))
                                                      )
                                                    : ['']
                                            }
                                            fullWidth
                                            value={assemblyFail.returnedBy?.toString()}
                                            onChange={handleFieldChange}
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
                                    <Grid item xs={6} />
                                    <Grid item xs={3}>
                                        <DatePicker
                                            value={assemblyFail.caDate}
                                            label="CA Date"
                                            onChange={value => {
                                                setEditStatus('edit');
                                                setAssemblyFail(a => ({
                                                    ...a,
                                                    caDate: value
                                                }));
                                            }}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <DatePicker
                                            value={assemblyFail.dateInvalid}
                                            onChange={value => {
                                                setEditStatus('edit');
                                                setAssemblyFail(a => ({
                                                    ...a,
                                                    dateInvalid: value
                                                }));
                                            }}
                                            label="Date Invalid"
                                        />
                                    </Grid>
                                </Fragment>
                            ) : (
                                <Fragment />
                            )}
                        </Fragment>
                    )
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
    snackbarVisible: PropTypes.bool,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    boardParts: PropTypes.arrayOf(PropTypes.shape({})),
    item: PropTypes.shape({}),
    itemErrors: PropTypes.arrayOf(
        PropTypes.shape({
            status: PropTypes.number,
            statusText: PropTypes.string,
            details: PropTypes.shape({}),
            item: PropTypes.string
        })
    ),
    worksOrdersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    pcasRevisions: PropTypes.arrayOf(PropTypes.shape({})),
    fetchPcasRevisionsForBoardPart: PropTypes.func,
    cits: PropTypes.arrayOf(PropTypes.shape({})),
    smtShifts: PropTypes.arrayOf(PropTypes.shape({})),
    employees: PropTypes.arrayOf(PropTypes.shape({})),
    faultCodes: PropTypes.arrayOf(PropTypes.shape({})),
    addItem: PropTypes.func,
    updateItem: PropTypes.func,
    itemId: PropTypes.string,
    searchWorksOrders: PropTypes.func.isRequired,
    worksOrdersSearchLoading: PropTypes.bool,
    clearWorksOrdersSearch: PropTypes.func.isRequired
};

AssemblyFail.defaultProps = {
    snackbarVisible: false,
    loading: null,
    profile: { employee: '', name: '' },
    boardParts: [],
    item: null,
    itemErrors: [],
    pcasRevisions: [],
    worksOrdersSearchResults: [],
    faultCodes: [],
    addItem: null,
    updateItem: null,
    itemId: null,
    cits: [],
    smtShifts: [],
    employees: [],
    fetchPcasRevisionsForBoardPart: null,
    worksOrdersSearchLoading: false
};

export default AssemblyFail;
