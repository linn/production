import React, { useState, useEffect } from 'react';
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
    Typeahead,
    Dropdown,
    useSearch,
    smartGoBack
} from '@linn-it/linn-form-components-library';
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
    partsSearchResults,
    partsSearchLoading,
    searchParts,
    clearPartsSearch,
    pcasRevisions,
    pcasRevisionsLoading,
    fetchPcasRevisionsForBoardPart,
    cits,
    smtShifts,
    smtShiftsLoading,
    employees,
    employeesLoading,
    faultCodes,
    faultCodesLoading,
    addItem,
    updateItem,
    itemId,
    history,
    previousPaths
}) {
    // Hooks
    const [assemblyFail, setAssemblyFail] = useState({
        numberOfFails: 1,
        dateTimeFound: new Date().toISOString(),
        dateTimeComplete: null,
        dateInvalid: null,
        caDate: null,
        boardPartNumber: ''
    });
    const [prevAssemblyFail, setPrevAssemblyFail] = useState({});

    useSearch(searchParts, assemblyFail.boardPartNumber, null);

    // Render Constants
    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';
    const aoiEscapeValues = ['Y', 'N'];
    const inputInvalid = () => !assemblyFail.worksOrderNumber;

    const getFaultCodesOptions = () => {
        const options = faultCodes.map(c => ({
            id: c.faultCode,
            displayText: `${c.faultCode} - ${c.description}`
        }));
        if (assemblyFail.faultCode && !options.some(f => f.id === assemblyFail.faultCode)) {
            options.push({ id: assemblyFail.faultCode, displayText: assemblyFail.faultCode });
        }
        return options;
    };

    const getEmployeeOptions = currentValue => {
        const options = employees
            .filter(c => !!c.fullName)
            .map(c => ({
                id: c.id,
                displayText: c.fullName
            }));
        if (currentValue && !options.some(e => e.id === currentValue)) {
            options.push({
                id: currentValue,
                displayText: `Employee ${currentValue} no longer valid`
            });
        }
        return options;
    };

    // Effects

    // initialisation
    useEffect(() => {
        if (editStatus !== 'create' && item && item !== prevAssemblyFail) {
            setAssemblyFail(item);
            setPrevAssemblyFail(item);
        }
    }, [item, prevAssemblyFail, editStatus]);

    // gets enteredBy from current user
    useEffect(() => {
        if (editStatus === 'create' && profile) {
            setAssemblyFail(a => ({
                ...a,
                enteredBy: profile?.employee.replace('/employees/', ''), // the current user
                enteredByName: profile?.name
            }));
        }
    }, [profile, editStatus]);

    // gets pcas revisions list for board part when it changes
    useEffect(() => {
        fetchPcasRevisionsForBoardPart(
            'searchTerm',
            assemblyFail.boardPartNumber ? assemblyFail.boardPartNumber : null
        );
    }, [assemblyFail.boardPartNumber, fetchPcasRevisionsForBoardPart]);

    // sets boardDescription when boardPart changes
    useEffect(() => {
        const exactMatch = partsSearchResults.find(
            p => p.partNumber === assemblyFail.boardPartNumber
        );
        if (assemblyFail.boardPartNumber === '') {
            setAssemblyFail(a => ({
                ...a,
                boardDescription: ''
            }));
        }
        if (exactMatch) {
            setAssemblyFail(f => ({
                ...f,
                boardDescription: exactMatch.description
            }));
        }
    }, [partsSearchResults, assemblyFail.boardPartNumber]);

    // sets circuitpartNumber when circuitRef changes
    useEffect(() => {
        if (assemblyFail.circuitRef) {
            setAssemblyFail(a => ({
                ...a,
                circuitPartNumber: pcasRevisions?.find(p => p.cref === a.circuitRef)?.partNumber
            }));
        }
    }, [pcasRevisions, assemblyFail.circuitRef]);

    // sets citResponsible when name is entered
    useEffect(() => {
        if (cits && assemblyFail.citResponsible) {
            setAssemblyFail(a => ({
                ...a,
                citResponsibleName: cits.find(c => c.code === a.citResponsible)?.name
            }));
        }
    }, [cits, assemblyFail.citResponsible]);

    // sets person responsible when name is entered
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

    // sets returnedBy when name is entered
    useEffect(() => {
        if (employees && assemblyFail.returnedBy) {
            setAssemblyFail(a => ({
                ...a,
                returnedByName: employees.find(e => Number(e.id) === Number(a.returnedBy).fullName)
            }));
        }
    }, [employees, assemblyFail.returnedBy]);

    // sets completedBy when name is entered
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

    // sets faultCode description when faultCode is entered
    useEffect(() => {
        if (faultCodes && assemblyFail.faultCode) {
            setAssemblyFail(a => ({
                ...a,
                faultCodeDescription: faultCodes.find(c => c.faultCode === a.faultCode)?.description
            }));
        }
    }, [faultCodes, assemblyFail.faultCode]);

    // Event Handlers
    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        if (propertyName === 'boardPartNumber') {
            setAssemblyFail({ ...assemblyFail, [propertyName]: newValue.toUpperCase() });
        } else {
            setAssemblyFail({ ...assemblyFail, [propertyName]: newValue });
        }
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
                enteredBy: profile?.employee.replace('/employees/', ''),
                enteredByName: profile?.name
            });
        } else {
            setAssemblyFail(item);
        }
        setEditStatus('view');
        clearWorksOrdersSearch('');
    };

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
                        <>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            {!creating() ? (
                                <>
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
                                    <Grid item xs={10} />
                                </>
                            ) : (
                                <></>
                            )}

                            <>
                                <Grid item xs={12}>
                                    <Typeahead
                                        onSelect={newValue => {
                                            setEditStatus('edit');
                                            setAssemblyFail(a => ({
                                                ...a,
                                                worksOrderNumber: newValue.orderNumber.toString(),
                                                partNumber: newValue.partNumber,
                                                partDescription: newValue.partDescription
                                            }));
                                        }}
                                        disabled={!creating()}
                                        label="Works Order"
                                        modal
                                        items={worksOrdersSearchResults}
                                        value={assemblyFail.worksOrderNumber}
                                        loading={worksOrdersSearchLoading}
                                        fetchItems={searchWorksOrders}
                                        links={false}
                                        minimumSearchTermLength={3}
                                        clearSearch={() => clearWorksOrdersSearch}
                                        placeholder="Enter Works Order Number"
                                    />
                                </Grid>
                            </>
                            {assemblyFail.worksOrderNumber || !creating() ? (
                                <>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
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
                                        />
                                    </Grid>
                                    <Grid item xs={6} />
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
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
                                    <Grid item xs={4} />
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
                                            items={getFaultCodesOptions()}
                                            fullWidth
                                            value={assemblyFail.faultCode || ''}
                                            onChange={handleFieldChange}
                                            optionsLoading={faultCodesLoading}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Dropdown
                                            label="Shift"
                                            propertyName="shift"
                                            disabled={smtShifts?.length === 0}
                                            items={smtShifts.map(s => ({
                                                id: s.shift,
                                                displayText: `${s.shift}  -  ${s.description}`
                                            }))}
                                            fullWidth
                                            value={assemblyFail.shift || ''}
                                            onChange={handleFieldChange}
                                            optionsLoading={smtShiftsLoading}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
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
                                            items={aoiEscapeValues}
                                            fullWidth
                                            value={assemblyFail.aoiEscape || ''}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Typeahead
                                            onSelect={newValue => {
                                                setEditStatus('edit');
                                                setAssemblyFail(a => ({
                                                    ...a,
                                                    boardPartNumber: newValue.partNumber,
                                                    boardDescription: newValue.partDescription
                                                }));
                                            }}
                                            label="Board or Assembly Part Number"
                                            modal
                                            items={partsSearchResults}
                                            value={assemblyFail.boardPartNumber}
                                            loading={partsSearchLoading}
                                            fetchItems={searchParts}
                                            links={false}
                                            clearSearch={() => clearPartsSearch}
                                            placeholder="Enter Board Part Number"
                                            minimumSearchTermLength={3}
                                            debounce={1000}
                                        />
                                    </Grid>
                                    <Grid item xs={5}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={
                                                partsSearchLoading
                                                    ? 'loading...'
                                                    : assemblyFail.boardDescription
                                            }
                                            label="Description"
                                            onChange={handleFieldChange}
                                            propertyName="boardDescription"
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
                                            value={assemblyFail.boardSerial}
                                            label="Board Serial"
                                            onChange={handleFieldChange}
                                            propertyName="boardSerial"
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Dropdown
                                            label="Circuit Ref"
                                            propertyName="circuitRef"
                                            disabled={pcasRevisions?.length === 0}
                                            items={pcasRevisions.map(p => ({
                                                id: p.cref,
                                                displayText: p.cref
                                            }))}
                                            fullWidth
                                            allowNoValue
                                            value={assemblyFail.circuitRef}
                                            onChange={handleFieldChange}
                                            optionsLoading={pcasRevisionsLoading}
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Typeahead
                                            onSelect={newValue => {
                                                setEditStatus('edit');
                                                setAssemblyFail(a => ({
                                                    ...a,
                                                    circuitPartNumber: newValue.partNumber
                                                }));
                                            }}
                                            label="Component"
                                            modal
                                            items={partsSearchResults}
                                            value={assemblyFail.circuitPartNumber}
                                            loading={partsSearchLoading}
                                            fetchItems={searchParts}
                                            links={false}
                                            clearSearch={() => clearPartsSearch}
                                            placeholder="Enter Part Number"
                                            minimumSearchTermLength={3}
                                            debounce={1000}
                                        />
                                    </Grid>
                                    <Grid item xs={5} />
                                    <Grid item xs={4}>
                                        <Dropdown
                                            label="CIT Responsible"
                                            propertyName="citResponsible"
                                            items={[
                                                ...new Set(
                                                    cits.map(c => ({
                                                        id: c.code,
                                                        displayText: `${c.code} - ${c.name}`
                                                    }))
                                                )
                                            ]}
                                            fullWidth
                                            value={assemblyFail.citResponsible || ''}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <Dropdown
                                            label="Person Responsible"
                                            type="number"
                                            propertyName="personResponsible"
                                            items={getEmployeeOptions(
                                                assemblyFail.personResponsible
                                            )}
                                            fullWidth
                                            allowNoValue
                                            value={assemblyFail.personResponsible}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={1} />
                                    <Grid item xs={4}>
                                        <DateTimePicker
                                            value={assemblyFail?.dateTimeComplete}
                                            label="Complete"
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
                                            items={getEmployeeOptions(assemblyFail.completedBy)}
                                            fullWidth
                                            allowNoValue
                                            value={assemblyFail.completedBy}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={2}>
                                        <InputField
                                            fullWidth
                                            value={assemblyFail.outSlot}
                                            label="Out Slot"
                                            onChange={handleFieldChange}
                                            propertyName="outSlot"
                                        />
                                    </Grid>
                                    <Grid item xs={2}>
                                        <InputField
                                            fullWidth
                                            value={assemblyFail.minsSpent}
                                            label="Engineer Mins Spent"
                                            type="number"
                                            onChange={handleFieldChange}
                                            propertyName="minsSpent"
                                        />
                                    </Grid>
                                    <Grid item xs={2} />
                                    <Grid item xs={2}>
                                        <Dropdown
                                            label="Returned By"
                                            type="number"
                                            propertyName="returnedBy"
                                            fullWidth
                                            items={getEmployeeOptions(assemblyFail.returnedBy)}
                                            value={assemblyFail.returnedBy}
                                            optionsLoading={employeesLoading}
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
                                </>
                            ) : (
                                <></>
                            )}
                        </>
                    )
                )}
                <Grid item xs={12}>
                    <SaveBackCancelButtons
                        saveDisabled={inputInvalid() || viewing()}
                        saveClick={handleSaveClick}
                        cancelClick={handleCancelClick}
                        backClick={() => {
                            smartGoBack(previousPaths, history.goBack);
                        }}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

AssemblyFail.propTypes = {
    history: PropTypes.shape({ goBack: PropTypes.func }).isRequired,
    profile: PropTypes.shape({ employee: PropTypes.string, name: PropTypes.string }),
    editStatus: PropTypes.string.isRequired,
    snackbarVisible: PropTypes.bool,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
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
    clearWorksOrdersSearch: PropTypes.func.isRequired,
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
    partsSearchLoading: PropTypes.bool,
    pcasRevisionsLoading: PropTypes.bool,
    smtShiftsLoading: PropTypes.bool,
    employeesLoading: PropTypes.bool,
    faultCodesLoading: PropTypes.bool,
    previousPaths: PropTypes.arrayOf(PropTypes.string)
};

AssemblyFail.defaultProps = {
    snackbarVisible: false,
    loading: null,
    profile: { employee: '', name: '' },
    partsSearchResults: [],
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
    worksOrdersSearchLoading: false,
    partsSearchLoading: false,
    pcasRevisionsLoading: false,
    smtShiftsLoading: false,
    employeesLoading: false,
    faultCodesLoading: false,
    previousPaths: []
};

export default AssemblyFail;
