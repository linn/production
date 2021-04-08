import React, { useState, useEffect, useCallback } from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import Grid from '@material-ui/core/Grid';
import Dialog from '@material-ui/core/Dialog';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import CloseIcon from '@material-ui/icons/Close';
import IconButton from '@material-ui/core/IconButton';
import {
    Title,
    ErrorCard,
    Loading,
    SnackbarMessage,
    InputField,
    TypeaheadDialog,
    CreateButton,
    SaveBackCancelButtons,
    SearchInputField,
    Dropdown,
    useSearch,
    DatePicker,
    smartGoBack
} from '@linn-it/linn-form-components-library';
import WorksOrderSerialNumbers from './WorksOrderSerialNumbers';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    marginTop: {
        marginTop: theme.spacing(2)
    },
    pullRight: {
        float: 'right'
    },
    printButton: {
        paddingRight: theme.spacing(2)
    },
    modal: { margin: theme.spacing(6), minWidth: theme.spacing(62) }
}));

function WorksOrder({
    item,
    profile,
    editStatus,
    worksOrderDetailsError,
    worksOrderError,
    history,
    loading,
    snackbarVisible,
    worksOrderDetails,
    employees,
    employeesLoading,
    partsSearchResults,
    partsSearchLoading,
    addItem,
    updateItem,
    setSnackbarVisible,
    fetchWorksOrderDetails,
    setEditStatus,
    fetchWorksOrder,
    searchParts,
    clearPartsSearch,
    itemErrors,
    printWorksOrderLabelsErrorDetail,
    printWorksOrderLabelsMessageVisible,
    printWorksOrderLabelsMessageText,
    printWorksOrderAioLabelsErrorDetail,
    printWorksOrderAioLabelsMessageVisible,
    printWorksOrderAioLabelsMessageText,
    printWorksOrderLabels,
    clearPrintWorksOrderLabelsErrors,
    setPrintWorksOrderLabelsMessageVisible,
    printWorksOrderAioLabels,
    clearPrintWorksOrderAioLabelsErrors,
    setPrintWorksOrderAioLabelsMessageVisible,
    setDefaultWorksOrderPrinter,
    defaultWorksOrderPrinter,
    clearErrors,
    serialNumbers,
    fetchSerialNumbers,
    options,
    previousPaths
}) {
    const [worksOrder, setWorksOrder] = useState({});
    const [prevWorksOrder, setPrevWorksOrder] = useState({});
    const [raisedByEmployee, setRaisedByEmployee] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [printerGroup, setPrinterGroup] = useState('Prod');
    const [viewSernos, setViewsernos] = useState(false);
    const [dialogOpen, setDialogOpen] = useState(false);
    const [cancelClicked, setCancelClicked] = useState(false);

    const printerGroups = ['Prod', 'DSM', 'Flexible', 'Kiko', 'LP12', 'Metalwork', 'SpeakerCover'];

    useSearch(fetchWorksOrder, searchTerm, null);

    const creating = useCallback(() => editStatus === 'create', [editStatus]);
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';

    const classes = useStyles();

    const cancelledByOptions = () => {
        const list = employees
            ?.filter(c => !!c.fullName)
            .map(c => ({
                id: c.id,
                displayText: c.fullName
            }));
        if (employees?.length && !employees?.find(e => e.id === worksOrder?.cancelledBy)) {
            list.push({ id: worksOrder?.cancelledBy, displayText: 'Name not found' });
        }
        return list;
    };

    useEffect(() => {
        if (item !== prevWorksOrder) {
            setPrevWorksOrder(item);

            setRaisedByEmployee(null);

            if (creating()) {
                setWorksOrder({ ...item, docType: 'WO', quantity: null });
                return;
            }

            setWorksOrder(item);

            if (item && item?.partNumber) {
                fetchWorksOrderDetails(encodeURI(item?.partNumber));
            }

            if (item && item?.orderNumber) {
                fetchSerialNumbers('documentNumber', item?.orderNumber);
            }
        }
    }, [item, prevWorksOrder, fetchWorksOrderDetails, fetchSerialNumbers, creating]);

    useEffect(() => {
        if (worksOrder && employees && worksOrder.raisedBy) {
            if (worksOrder.raisedBy) {
                setRaisedByEmployee(
                    employees.find(employee => employee.id === worksOrder.raisedBy)
                );
            }
        }
    }, [employees, worksOrder]);

    useEffect(() => {
        if (creating() && worksOrderDetails) {
            setWorksOrder(wo => ({
                ...wo,
                workStationCode: worksOrderDetails?.workStationCode,
                raisedByDepartment: worksOrderDetails?.departmentCode,
                quantity: worksOrderDetails?.quantityToBuild,
                quantityBuilt: 0
            }));
        }
    }, [worksOrderDetails, editStatus, creating]);

    useEffect(() => {
        if (creating() && options.partNumber && !worksOrder.partNumber && !cancelClicked) {
            fetchWorksOrderDetails(options.partNumber);
            setWorksOrder({
                ...worksOrder,
                docType: 'WO',
                partNumber: options.partNumber
            });
        }
    }, [options, creating, worksOrder, fetchWorksOrderDetails, cancelClicked]);

    const handleCancelClick = () => {
        setCancelClicked(!cancelClicked);
        setWorksOrder({ ...item, docType: 'WO' });
        setEditStatus('view');
    };

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(worksOrder.orderNumber, worksOrder);
            setEditStatus('view');
        } else if (creating()) {
            addItem(worksOrder);
            setEditStatus('view');
        }
    };

    const handlePartSelect = part => {
        fetchWorksOrderDetails(encodeURIComponent(part.partNumber));
        setWorksOrder({ ...worksOrder, partNumber: part.partNumber });
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'printer') {
            setPrinterGroup(newValue);
            setDefaultWorksOrderPrinter(newValue);
            return;
        }

        if (viewing()) {
            setEditStatus('edit');
        }

        if (propertyName === 'searchTerm') {
            setSearchTerm(newValue);
            clearErrors();
            return;
        }

        if (propertyName === 'part') {
            fetchWorksOrderDetails(newValue);
        }

        setWorksOrder({ ...worksOrder, [propertyName]: newValue });
    };

    const createValid = () =>
        worksOrder.docType &&
        worksOrder.partNumber &&
        worksOrder.workStationCode &&
        worksOrder.quantity &&
        worksOrder.raisedByDepartment &&
        creating();

    const updateValid = () => editing() && (worksOrder.reasonCancelled || worksOrder.quantity);

    const handlePrintWorksOrderLabelsClick = () => {
        clearPrintWorksOrderLabelsErrors();
        printWorksOrderLabels({ orderNumber: worksOrder.orderNumber, printerGroup });
    };

    const handlePrintWorksOrderAioLabelsClick = () => {
        clearPrintWorksOrderAioLabelsErrors();
        printWorksOrderAioLabels({ orderNumber: worksOrder.orderNumber });
    };

    const handleViewSernosClick = () => {
        setViewsernos(!viewSernos);
    };

    const cancellationFields = () => (
        <>
            <Grid item xs={4}>
                <Dropdown
                    label="Cancelled By"
                    type="number"
                    propertyName="cancelledBy"
                    disabled={!dialogOpen && worksOrder?.cancelledBy}
                    allowNoValue
                    items={cancelledByOptions()}
                    fullWidth
                    value={worksOrder?.cancelledBy}
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={4}>
                <DatePicker
                    value={
                        worksOrder?.dateCancelled
                            ? worksOrder.dateCancelled
                            : new Date().toISOString()
                    }
                    label="Date Cancelled"
                    disabled={!dialogOpen && worksOrder?.dateCancelled}
                    onChange={value => {
                        setEditStatus('edit');
                        setWorksOrder(a => ({
                            ...a,
                            dateCancelled: value
                        }));
                    }}
                />
            </Grid>
            <Grid item xs={8} />
            <Grid item xs={4}>
                <InputField
                    fullWidth
                    required={editing()}
                    value={worksOrder?.reasonCancelled}
                    disabled={!dialogOpen && !!worksOrder?.reasonCancelled}
                    label="Reason Cancelled"
                    helperText={editing() ? 'Reason is required if cancelling a works order' : ''}
                    propertyName="reasonCancelled"
                    onChange={handleFieldChange}
                />
            </Grid>
            <Grid item xs={8} />
        </>
    );

    return (
        <Page>
            <Dialog
                data-testid="modal"
                open={dialogOpen}
                className={classes.modal}
                onClose={() => {
                    setWorksOrder(w => ({
                        ...w,
                        dateCancelled: null,
                        reasonCancelled: null,
                        cancelledBy: null
                    }));
                    setDialogOpen(false);
                }}
                fullWidth
                maxWidth="md"
            >
                <div className={classes.modal}>
                    <IconButton
                        className={classes.pullRight}
                        aria-label="Close"
                        onClick={() => setDialogOpen(false)}
                    >
                        <CloseIcon />
                    </IconButton>
                    <Typography variant="h5" gutterBottom>
                        Cancellation Details
                    </Typography>
                    {cancellationFields()}
                    <Grid item xs={12}>
                        <Button
                            className={classes.printButton}
                            onClick={() => {
                                setDialogOpen(false);
                                handleSaveClick();
                            }}
                            variant="outlined"
                            color="secondary"
                            disabled={
                                !worksOrder?.reasonCancelled ||
                                !worksOrder?.dateCancelled ||
                                !worksOrder?.cancelledBy
                            }
                        >
                            Confirm
                        </Button>
                    </Grid>
                </div>
            </Dialog>
            <Grid container spacing={3}>
                <SnackbarMessage
                    visible={printWorksOrderLabelsMessageVisible}
                    onClose={() => setPrintWorksOrderLabelsMessageVisible(false)}
                    message={printWorksOrderLabelsMessageText}
                />
                <SnackbarMessage
                    visible={printWorksOrderAioLabelsMessageVisible}
                    onClose={() => setPrintWorksOrderAioLabelsMessageVisible(false)}
                    message={printWorksOrderAioLabelsMessageText}
                />
                <Grid item xs={12}>
                    {creating() ? (
                        <>
                            <Title text="Raise Works Order" />
                            <Button
                                color="primary"
                                variant="outlined"
                                style={{ float: 'right' }}
                                onClick={() => {
                                    history.push('/production/works-orders');
                                    setEditStatus('view');
                                }}
                            >
                                Search
                            </Button>
                        </>
                    ) : (
                        <>
                            <Title text="Works Order" />
                            <CreateButton createUrl="/production/works-orders/create" />
                        </>
                    )}
                </Grid>
                {itemErrors &&
                    itemErrors.map((error, index) => (
                        // eslint-disable-next-line react/no-array-index-key
                        <Grid item xs={12} key={index}>
                            <ErrorCard
                                errorMessage={`${printWorksOrderLabelsErrorDetail ||
                                    printWorksOrderAioLabelsErrorDetail ||
                                    worksOrderError ||
                                    ''} `}
                            />
                        </Grid>
                    ))}
                {worksOrderDetailsError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={worksOrderDetailsError} />
                    </Grid>
                )}
                {!creating() && (
                    <>
                        <Grid item xs={4}>
                            <SearchInputField
                                label="Search for Order Number"
                                fullWidth
                                placeHolder="Order Number"
                                onChange={handleFieldChange}
                                propertyName="searchTerm"
                                type="number"
                                value={searchTerm}
                            />
                        </Grid>
                        <Grid item xs={8} />
                    </>
                )}
                {loading || employeesLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    (worksOrder || creating()) && (
                        <>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            {!creating() && (
                                <>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={worksOrder.orderNumber}
                                            label="Order Number"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                </>
                            )}
                            {!creating() && (
                                <>
                                    <Grid item xs={4}>
                                        <Dropdown
                                            fullWidth
                                            items={printerGroups}
                                            label="Label Printer Group"
                                            value={defaultWorksOrderPrinter || 'Prod'}
                                            onChange={handleFieldChange}
                                            propertyName="printer"
                                            allowNoValue
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                    <Grid item xs={12}>
                                        <Button
                                            className={classes.printButton}
                                            onClick={handlePrintWorksOrderLabelsClick}
                                            variant="outlined"
                                            color="primary"
                                        >
                                            Print Labels
                                        </Button>
                                        <Button
                                            onClick={handlePrintWorksOrderAioLabelsClick}
                                            variant="outlined"
                                            color="primary"
                                        >
                                            Print AIO Labels
                                        </Button>
                                    </Grid>
                                </>
                            )}
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={worksOrder.docType}
                                    propertyName="docType"
                                    onChange={handleFieldChange}
                                    label="Type"
                                />
                            </Grid>
                            <Grid item xs={8} />
                            {worksOrderDetails && worksOrderDetails?.auditDisclaimer && (
                                <>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            error
                                            value={worksOrderDetails?.auditDisclaimer}
                                            label="Audit Disclaimer"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                </>
                            )}
                            <>
                                <Grid item xs={4}>
                                    <InputField
                                        disabled
                                        label="Part"
                                        maxLength={14}
                                        fullWidth
                                        value={worksOrder.partNumber}
                                        onChange={handleFieldChange}
                                        propertyName="partSearchTerm"
                                    />
                                </Grid>
                                {creating() && (
                                    <Grid item xs={1}>
                                        <div className={classes.marginTop}>
                                            <TypeaheadDialog
                                                title="Search For Part"
                                                onSelect={handlePartSelect}
                                                searchItems={partsSearchResults || []}
                                                loading={partsSearchLoading}
                                                fetchItems={searchParts}
                                                clearSearch={() => clearPartsSearch}
                                            />
                                        </div>
                                    </Grid>
                                )}
                                <Grid item xs={7}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={
                                            worksOrderDetails
                                                ? worksOrderDetails?.partDescription
                                                : ''
                                        }
                                        label="Description"
                                    />
                                </Grid>
                            </>
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={
                                        creating()
                                            ? worksOrderDetails &&
                                              worksOrderDetails?.workStationCode
                                            : worksOrder.workStationCode
                                    }
                                    label="Work Station"
                                    onChange={handleFieldChange}
                                    propertyName="workStationCode"
                                />
                            </Grid>
                            <Grid item xs={8} />
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    required={creating() || editing()}
                                    value={worksOrder.quantity}
                                    propertyName="quantity"
                                    label="Quantity"
                                    type="number"
                                    helperText={creating() || editing() ? 'Required' : ''}
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            {!creating() ? (
                                <>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={worksOrder.quantityBuilt}
                                            label="Quantity Built"
                                            propertyName="quantityBuilt"
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={4} />
                                </>
                            ) : (
                                <Grid item xs={8} />
                            )}
                            {!creating() && (
                                <>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={worksOrder.batchNumber}
                                            label="Batch Number"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={worksOrder.startedByShift}
                                            label="Started By Shift"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={raisedByEmployee && raisedByEmployee.fullName}
                                            label="Raised By"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={
                                                worksOrder.dateRaised &&
                                                moment(worksOrder.dateRaised).format('DD-MMM-YYYY')
                                            }
                                            label="Date Raised"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                </>
                            )}
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={
                                        worksOrderDetails
                                            ? worksOrderDetails?.departmentDescription
                                            : ''
                                    }
                                    label="Department"
                                />
                            </Grid>
                            <Grid item xs={8} />
                            {!creating() && (
                                <>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={worksOrder.kittedShort}
                                            label="Kitted Short"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />{' '}
                                </>
                            )}
                            {worksOrder?.dateCancelled && !dialogOpen ? (
                                <>{cancellationFields()}</>
                            ) : (
                                <> </>
                            )}
                            <Grid item xs={12}>
                                <Button
                                    className={classes.printButton}
                                    onClick={handleViewSernosClick}
                                    variant="outlined"
                                    disabled={!serialNumbers.length}
                                >
                                    View Serial Numbers
                                </Button>
                            </Grid>
                            {!worksOrder.dateCancelled && (
                                <Grid item xs={12}>
                                    <Button
                                        className={classes.printButton}
                                        onClick={() => {
                                            setWorksOrder(w => ({
                                                ...w,
                                                cancelledBy: profile?.employee.replace(
                                                    '/employees/',
                                                    ''
                                                ),
                                                dateCancelled: new Date().toISOString()
                                            }));
                                            setDialogOpen(true);
                                        }}
                                        variant="outlined"
                                        color="secondary"
                                        disabled={!!worksOrder.dateCancelled}
                                    >
                                        Cancel Works Order
                                    </Button>
                                </Grid>
                            )}
                            {viewSernos && (
                                <WorksOrderSerialNumbers
                                    employees={employees}
                                    serialNumbers={serialNumbers}
                                />
                            )}
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    value={worksOrder.batchNotes}
                                    label="Batch Notes"
                                    propertyName="batchNotes"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={8} />
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || !(createValid() || updateValid())}
                                    saveClick={handleSaveClick}
                                    cancelClick={handleCancelClick}
                                    backClick={() => {
                                        smartGoBack(previousPaths, history.goBack);
                                    }}
                                />
                            </Grid>
                        </>
                    )
                )}
            </Grid>
        </Page>
    );
}

WorksOrder.propTypes = {
    item: PropTypes.shape({ orderNumber: PropTypes.number, partNumber: PropTypes.string }),
    worksOrderDetails: PropTypes.shape({
        workStationCode: PropTypes.string,
        departmentCode: PropTypes.string,
        quantityToBuild: PropTypes.string,
        auditDisclaimer: PropTypes.string,
        partDescription: PropTypes.string,
        departmentDescription: PropTypes.string
    }),
    editStatus: PropTypes.string.isRequired,
    worksOrderDetailsError: PropTypes.string,
    worksOrderError: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    loading: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    fetchWorksOrderDetails: PropTypes.func.isRequired,
    setEditStatus: PropTypes.func.isRequired,
    addItem: PropTypes.func.isRequired,
    updateItem: PropTypes.func.isRequired,
    history: PropTypes.shape({ goBack: PropTypes.func, push: PropTypes.func }).isRequired,
    fetchWorksOrder: PropTypes.func.isRequired,
    searchParts: PropTypes.func,
    clearPartsSearch: PropTypes.func,
    employees: PropTypes.arrayOf(PropTypes.shape({})),
    employeesLoading: PropTypes.bool,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    partsSearchLoading: PropTypes.bool,
    itemErrors: PropTypes.arrayOf(PropTypes.shape({})),
    printWorksOrderLabelsErrorDetail: PropTypes.string,
    printWorksOrderLabelsMessageVisible: PropTypes.bool,
    printWorksOrderLabelsMessageText: PropTypes.string,
    printWorksOrderAioLabelsErrorDetail: PropTypes.string,
    printWorksOrderAioLabelsMessageVisible: PropTypes.bool,
    printWorksOrderAioLabelsMessageText: PropTypes.string,
    printWorksOrderLabels: PropTypes.func.isRequired,
    clearPrintWorksOrderLabelsErrors: PropTypes.func.isRequired,
    setPrintWorksOrderLabelsMessageVisible: PropTypes.func.isRequired,
    printWorksOrderAioLabels: PropTypes.func.isRequired,
    clearPrintWorksOrderAioLabelsErrors: PropTypes.func.isRequired,
    setPrintWorksOrderAioLabelsMessageVisible: PropTypes.func.isRequired,
    clearErrors: PropTypes.func.isRequired,
    setDefaultWorksOrderPrinter: PropTypes.func,
    defaultWorksOrderPrinter: PropTypes.oneOfType([PropTypes.string, PropTypes.shape({})]),
    fetchSerialNumbers: PropTypes.func,
    serialNumbers: PropTypes.arrayOf(PropTypes.shape()),
    options: PropTypes.shape({ partNumber: PropTypes.string }),
    profile: PropTypes.shape({ employee: PropTypes.string }),
    previousPaths: PropTypes.arrayOf(PropTypes.string)
};

WorksOrder.defaultProps = {
    item: {},
    profile: null,
    worksOrderDetails: null,
    worksOrderDetailsError: null,
    worksOrderError: null,
    snackbarVisible: false,
    loading: false,
    employees: null,
    employeesLoading: false,
    partsSearchResults: null,
    partsSearchLoading: false,
    itemErrors: null,
    printWorksOrderLabelsErrorDetail: '',
    printWorksOrderLabelsMessageVisible: false,
    printWorksOrderLabelsMessageText: '',
    printWorksOrderAioLabelsErrorDetail: '',
    printWorksOrderAioLabelsMessageVisible: false,
    printWorksOrderAioLabelsMessageText: '',
    searchParts: null,
    clearPartsSearch: null,
    defaultWorksOrderPrinter: 'Prod',
    serialNumbers: [],
    options: {},
    fetchSerialNumbers: null,
    setDefaultWorksOrderPrinter: null,
    previousPaths: []
};

export default WorksOrder;
