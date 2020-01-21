import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import {
    SaveBackCancelButtons,
    TableWithInlineEditing,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    Typeahead,
    Dropdown,
    DatePicker
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function AteTest({
    editStatus,
    itemError,
    history,
    profile,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    setSnackbarVisible,
    worksOrdersSearchResults,
    worksOrdersSearchLoading,
    searchWorksOrders,
    clearWorksOrdersSearch,
    employees,
    ateFaultCodes,
    componentCounts,
    getComponentCounts,
    detailParts,
    getDetailParts
}) {
    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    const [ateTest, setAteTest] = useState({
        pcbOperator: null,
        details: [],
        numberTested: 0
    });
    const [prevAteTest, setPrevAteTest] = useState({});

    const dpmo = (instances, failures) => Math.round((1000000 / instances) * failures);

    useEffect(() => {
        if (editStatus !== 'create' && item && item !== prevAteTest) {
            setAteTest(item);
            setPrevAteTest(item);
        }
    }, [item, prevAteTest, editStatus]);

    useEffect(() => {
        if (editStatus === 'create' && profile) {
            setAteTest(a => ({
                ...a,
                userNumber: profile.employee.replace('/employees/', ''), // the current user
                userName: profile.name
            }));
        }
    }, [profile, editStatus]);

    useEffect(() => {
        if (employees && ateTest.pcbOperator) {
            setAteTest(a => ({
                ...a,
                pcbOperatorName: employees.find(e => Number(e.id) === Number(a.pcbOperator))
                    ?.fullName
            }));
        }
    }, [employees, ateTest.pcbOperator]);

    useEffect(() => {
        if (ateTest.partNumber) {
            getDetailParts('searchTerm', ateTest.partNumber);
        }
    }, [ateTest.partNumber, getDetailParts]);

    useEffect(() => {
        if (ateTest.partNumber) {
            getComponentCounts(ateTest.partNumber);
        }
    }, [getComponentCounts, ateTest.partNumber]);

    useEffect(() => {
        const worksOrder = worksOrdersSearchResults?.find(
            w => w.orderNumber === ateTest.worksOrderNumber
        );
        if (editStatus === 'create') {
            setAteTest(a => ({
                ...a,
                numberTested: worksOrder?.qtyTested,
                worksOrderQuantity: worksOrder?.quantity
            }));
        }
    }, [ateTest.worksOrderNumber, setAteTest, editStatus, worksOrdersSearchResults]);

    const handleDetailFieldChange = (propertyName, newValue) => {
        setAteTest({ ...ateTest, [propertyName]: newValue });
        if (viewing()) {
            setEditStatus('edit');
        }
    };

    const updateOp = details => {
        handleDetailFieldChange('details', details);
    };

    const inputInvalid = () =>
        !ateTest.worksOrderNumber ||
        !ateTest.pcbOperator ||
        !ateTest.userNumber ||
        ateTest.details.some(d => !d.circuitRef || !d.aoiEscape);

    const Table = () => {
        const tableColumns = [
            {
                title: 'No.',
                key: 'itemNumber',
                type: 'number',
                notEditable: true
            },
            {
                title: 'Board Fail No.',
                key: 'boardFailNumber',
                type: 'number'
            },
            {
                title: 'Fails',
                key: 'numberOfFails',
                type: 'number'
            },
            {
                title: 'Circuit Ref',
                key: 'circuitRef',
                type: 'dropdown',
                options: detailParts?.map(p => ({
                    id: p.cref,
                    displayText: `${p.cref} - ${p.partNumber}`
                }))
            },
            {
                title: 'Fault Code',
                key: 'ateTestFaultCode',
                type: 'dropdown',
                options: ateFaultCodes.map(f => f.faultCode)
            },
            {
                title: 'Type',
                key: 'smtOrPcb',
                type: 'dropdown',
                options: [
                    'SMT',
                    'PCB',
                    'FLOW SOLDER',
                    'TEST',
                    'REPAIR',
                    'CF',
                    'ZOT',
                    'ATE',
                    'PROCESS',
                    'SUPPORT'
                ]
            },
            {
                title: 'Shift',
                key: 'shift',
                type: 'text'
            },
            {
                title: 'Batch',
                key: 'batchNumber',
                type: 'number'
            },
            {
                title: 'AOI Escape',
                key: 'aoiEscape',
                type: 'dropdown',
                required: true,
                options: ['', 'Y', 'N']
            },
            {
                title: 'PCB Operator',
                key: 'pcbOperator',
                type: 'dropdown',
                options: employees.map(e => ({ id: e.id, displayText: e.fullName }))
            },
            {
                title: 'Comments',
                key: 'comments',
                type: 'text'
            },
            {
                title: 'Corrective Action',
                key: 'correctiveAction',
                type: 'text'
            },
            {
                title: 'Board SN',
                key: 'boardSerialNumber',
                type: 'text'
            }
        ];

        return (
            <Grid item xs={12}>
                <Typography variant="h5">Details</Typography>

                <TableWithInlineEditing
                    columnsInfo={tableColumns}
                    content={ateTest.details.map((o, i) => ({
                        ...o,
                        id: o.itemNumber ? o.itemNumber : i + 1
                    }))}
                    updateContent={updateOp}
                    editStatus={editStatus}
                    allowedToEdit
                    allowedToCreate
                    allowedToDelete={creating()}
                />
            </Grid>
        );
    };

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, ateTest);
            setEditStatus('view');
        } else if (creating()) {
            addItem(ateTest);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        if (creating()) {
            setAteTest({ pcbOperator: null, details: [] });
        } else {
            setAteTest(item);
            setEditStatus('view');
        }
    };

    const handleBackClick = () => {
        history.goBack();
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setAteTest({ ...ateTest, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Log ATE Test Results" />
                    ) : (
                        <Title text="ATE Test Results" />
                    )}
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    ateTest &&
                    itemError?.faultCode !== 404 && (
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
                                            value={ateTest.testId}
                                            label="Id"
                                            disabled
                                            onChange={handleFieldChange}
                                            propertyName="testId"
                                        />
                                    </Grid>
                                    <Grid item xs={10} />{' '}
                                </Fragment>
                            ) : (
                                <Fragment />
                            )}
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    value={creating() ? profile?.name : ateTest.userName}
                                    label="Entered By"
                                    disabled
                                    onChange={handleFieldChange}
                                    propertyName="userName"
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <DatePicker
                                    value={ateTest.flowSolderDate}
                                    label="Flow Solder Date"
                                    onChange={value => {
                                        setEditStatus('edit');
                                        setAteTest(a => ({
                                            ...a,
                                            flowSolderDate: value
                                        }));
                                    }}
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <DatePicker
                                    value={ateTest.dateTested}
                                    label="Date Tested"
                                    onChange={value => {
                                        setEditStatus('edit');
                                        setAteTest(a => ({
                                            ...a,
                                            dateTested: value
                                        }));
                                    }}
                                />
                            </Grid>
                            <Grid itemxs={2} />
                            <Grid item xs={3}>
                                <Typeahead
                                    onSelect={newValue => {
                                        setEditStatus('edit');
                                        setAteTest(a => ({
                                            ...a,
                                            worksOrderNumber: newValue.name,
                                            partNumber: newValue.partNumber,
                                            partDescription: newValue.partDescription
                                        }));
                                    }}
                                    label="Works Order"
                                    modal
                                    disabled={!creating()}
                                    items={worksOrdersSearchResults.map(w => ({
                                        ...w,
                                        name: w.orderNumber,
                                        description: w.partNumber
                                    }))}
                                    value={`${
                                        ateTest.worksOrderNumber ? ateTest.worksOrderNumber : ''
                                    }`}
                                    loading={worksOrdersSearchLoading}
                                    fetchItems={searchWorksOrders}
                                    links={false}
                                    clearSearch={() => clearWorksOrdersSearch}
                                    placeholder="Search By Order Number"
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={ateTest?.worksOrderQuantity}
                                    label="Qty"
                                    onChange={() => {}}
                                    propertyName="worksOrderQuantity"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={ateTest.partNumber}
                                    label="Part Number"
                                    onChange={handleFieldChange}
                                    propertyName="partNumber"
                                />
                            </Grid>
                            <Grid item xs={6}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={ateTest.partDescription}
                                    label="Part Description"
                                    onChange={handleFieldChange}
                                    propertyName="partDescription"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={ateTest.numberTested}
                                    label="Number Tested"
                                    onChange={handleFieldChange}
                                    propertyName="numberTested"
                                />
                            </Grid>
                            <Grid item xs={1} />
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={ateTest.numberOfPcbBoardFails}
                                    label="No. PCB Fails"
                                    onChange={handleFieldChange}
                                    propertyName="numberOfPcbBoardFails"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={`${Math.round(
                                        ((ateTest.numberTested - ateTest.numberOfPcbBoardFails) /
                                            ateTest.numberTested) *
                                            1000
                                    ) / 10}%`}
                                    label="PCB Pass Rate"
                                    onChange={handleFieldChange}
                                    propertyName="pcbPassRate"
                                />
                            </Grid>
                            <Grid item xs={1} />
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={ateTest.numberOfSmtBoardFails}
                                    label="No. SMT Fails"
                                    onChange={handleFieldChange}
                                    propertyName="numberOfSmtBoardFails"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={`${Math.round(
                                        ((ateTest.numberTested - ateTest.numberOfSmtBoardFails) /
                                            ateTest.numberTested) *
                                            1000
                                    ) / 10}%`}
                                    label="SMT Pass Rate"
                                    onChange={handleFieldChange}
                                    propertyName="smtPassRate"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={componentCounts?.smtComponents * ateTest.numberTested}
                                    label="No. SMT Components"
                                    onChange={handleFieldChange}
                                    disabled
                                    propertyName="numberOfSmtComponents"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={ateTest.numberOfSmtFails}
                                    label="No. SMT Fails"
                                    onChange={handleFieldChange}
                                    propertyName="numberOfSmtFails"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={dpmo(
                                        componentCounts?.smtComponents * ateTest.numberTested,
                                        ateTest.numberOfSmtFails
                                    )}
                                    label="SMT DPMO"
                                    onChange={handleFieldChange}
                                    propertyName="SMT DPMO"
                                />
                            </Grid>
                            <Grid item xs={6} />
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={componentCounts?.pcbComponents * ateTest.numberTested}
                                    disabled
                                    label="No. PCB Components"
                                    onChange={handleFieldChange}
                                    propertyName="numberOfPcbComponents"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={ateTest.numberOfPcbFails}
                                    label="No. PCB Fails"
                                    onChange={handleFieldChange}
                                    propertyName="numberOfPcbFails"
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={dpmo(
                                        componentCounts?.pcbComponents * ateTest.numberTested,
                                        ateTest.numberOfPcbFails
                                    )}
                                    label="PCB DPMO"
                                    onChange={handleFieldChange}
                                    propertyName="pcbDPMO"
                                />
                            </Grid>
                            <Grid item xs={2} />
                            <Grid item xs={4}>
                                <Dropdown
                                    label="PCB Operator"
                                    type="number"
                                    propertyName="pcbOperator"
                                    allowNoValue
                                    items={employees
                                        .filter(c => !!c.fullName)
                                        .map(c => ({
                                            id: c.id,
                                            displayText: c.fullName
                                        }))}
                                    fullWidth
                                    value={ateTest.pcbOperator}
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <InputField
                                    fullWidth
                                    value={ateTest.minutesSpent}
                                    label="Minutes Spent"
                                    onChange={handleFieldChange}
                                    propertyName="minutesSpent"
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <Dropdown
                                    label="Flow Machine"
                                    propertyName="flowMachine"
                                    items={['FLOW SOLDER', 'NON FLOW SOLDER']}
                                    fullWidth
                                    value={ateTest.flowMachine ? ateTest.flowMachine : ''}
                                    allowNoValue
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <Dropdown
                                    label="Machine"
                                    propertyName="machine"
                                    items={['GENRAD', 'TAKAYA']}
                                    fullWidth
                                    value={ateTest.machine ? ateTest.machine : ''}
                                    allowNoValue
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <Dropdown
                                    label="Place Found"
                                    propertyName="placeFound"
                                    items={['ATE', 'SMT']}
                                    fullWidth
                                    value={ateTest.placeFound ? ateTest.placeFound : ''}
                                    allowNoValue
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={1} />
                            {Table()}
                        </Fragment>
                    )
                )}
                <Grid item xs={12}>
                    <SaveBackCancelButtons
                        saveDisabled={viewing() || inputInvalid()}
                        saveClick={handleSaveClick}
                        cancelClick={handleCancelClick}
                        backClick={handleBackClick}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

AteTest.propTypes = {
    item: PropTypes.shape({
        ateTest: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string
    }),
    history: PropTypes.shape({ goBack: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    employees: PropTypes.arrayOf(PropTypes.shape({})),
    ateFaultCodes: PropTypes.arrayOf(PropTypes.shape({})),
    profile: PropTypes.shape({ name: PropTypes.string, employee: PropTypes.string }),
    worksOrdersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    worksOrdersSearchLoading: PropTypes.bool,
    searchWorksOrders: PropTypes.func,
    clearWorksOrdersSearch: PropTypes.func,
    componentCounts: PropTypes.shape({
        smtComponents: PropTypes.number,
        pcbComponents: PropTypes.number
    }),
    getComponentCounts: PropTypes.func.isRequired,
    getDetailParts: PropTypes.func.isRequired,
    detailParts: PropTypes.arrayOf(PropTypes.shape({}))
};

AteTest.defaultProps = {
    item: null,
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null,
    employees: [],
    ateFaultCodes: [],
    profile: { name: '' },
    worksOrdersSearchResults: null,
    worksOrdersSearchLoading: false,
    searchWorksOrders: PropTypes.arrayOf(PropTypes.shape({})),
    clearWorksOrdersSearch: PropTypes.func,
    componentCounts: { smtComponents: 0, pcbComponents: 0 },
    detailParts: []
};

export default AteTest;
