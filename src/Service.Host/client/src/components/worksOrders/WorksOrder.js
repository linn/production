import React, { Fragment, useState, useEffect, useCallback } from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
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
    useSearch
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    marginTop: {
        marginTop: theme.spacing(2)
    }
}));

function WorksOrder({
    item,
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
    clearPartsSearch
}) {
    const [worksOrder, setWorksOrder] = useState({});
    const [prevWorksOrder, setPrevWorksOrder] = useState({});
    const [raisedByEmployee, setRaisedByEmployee] = useState(null);
    const [cancelledByEmployee, setCancelledByEmployee] = useState(null);
    const [searchTerm, setSearchTerm] = useState(null);

    useSearch(fetchWorksOrder, searchTerm, null);

    const creating = useCallback(() => editStatus === 'create', [editStatus]);
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';

    const classes = useStyles();

    useEffect(() => {
        if (item !== prevWorksOrder) {
            setPrevWorksOrder(item);

            setRaisedByEmployee(null);
            setCancelledByEmployee(null);

            if (creating()) {
                setWorksOrder({ ...item, docType: 'WO', quantity: null });
                return;
            }

            setWorksOrder(item);

            if (item && item.partNumber) {
                fetchWorksOrderDetails(encodeURI(item.partNumber));
            }
        }
    }, [item, prevWorksOrder, fetchWorksOrderDetails, creating]);

    useEffect(() => {
        if (worksOrder && employees && worksOrder.raisedBy) {
            if (worksOrder.raisedBy) {
                setRaisedByEmployee(
                    employees.find(employee => employee.id === worksOrder.raisedBy)
                );
            }
            if (worksOrder.cancelledBy) {
                setCancelledByEmployee(
                    employees.find(employee => employee.id === worksOrder.cancelledBy)
                );
            }
        }
    }, [employees, worksOrder]);

    useEffect(() => {
        if (creating() && worksOrderDetails) {
            setWorksOrder(wo => ({
                ...wo,
                workStationCode: worksOrderDetails.workStationCode,
                departmentCode: worksOrderDetails.departmentCode,
                quantity: worksOrderDetails.quantity,
                quantityBuilt: 0
            }));
        }
    }, [worksOrderDetails, editStatus, creating]);

    const handleCancelClick = () => {
        setWorksOrder(item);
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

    const handleBackClick = () => {
        setEditStatus('view');
        history.push('/production/works-orders');
    };

    const handlePartSelect = part => {
        fetchWorksOrderDetails(encodeURIComponent(part.partNumber));
        setWorksOrder({ ...worksOrder, partNumber: part.partNumber });
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }

        if (propertyName === 'searchTerm') {
            setSearchTerm(newValue);
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
        worksOrder.departmentCode &&
        creating();

    const updateValid = () => editing() && (worksOrder.reasonCancelled || worksOrder.quantity);

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Fragment>
                            <Title text="Raise Works Order" />
                            <Button
                                color="primary"
                                variant="outlined"
                                style={{ float: 'right' }}
                                onClick={handleBackClick}
                            >
                                Search
                            </Button>
                        </Fragment>
                    ) : (
                        <Fragment>
                            <Title text="Works Order" />
                            <CreateButton createUrl="/production/works-orders/create" />
                        </Fragment>
                    )}
                </Grid>
                {worksOrderError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={worksOrderError} />
                    </Grid>
                )}
                {worksOrderDetailsError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={worksOrderDetailsError} />
                    </Grid>
                )}
                {!creating() && (
                    <Fragment>
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
                    </Fragment>
                )}
                {loading || employeesLoading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    (worksOrder || creating()) && (
                        <Fragment>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            {!creating() && (
                                <Fragment>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={worksOrder.orderNumber}
                                            label="Order Number"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                </Fragment>
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
                            {worksOrderDetails && worksOrderDetails.auditDisclaimer && (
                                <Fragment>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            error
                                            value={worksOrderDetails.auditDisclaimer}
                                            label="Audit Disclaimer"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                </Fragment>
                            )}
                            <Fragment>
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
                                                ? worksOrderDetails.partDescription
                                                : ''
                                        }
                                        label="Description"
                                    />
                                </Grid>
                            </Fragment>
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={
                                        creating()
                                            ? worksOrderDetails && worksOrderDetails.workStationCode
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
                                <Fragment>
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
                                </Fragment>
                            ) : (
                                <Grid item xs={8} />
                            )}
                            {!creating() && (
                                <Fragment>
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
                                </Fragment>
                            )}
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={
                                        worksOrderDetails
                                            ? worksOrderDetails.departmentDescription
                                            : ''
                                    }
                                    label="Department"
                                />
                            </Grid>
                            <Grid item xs={8} />
                            {!creating() && (
                                <Fragment>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={
                                                cancelledByEmployee && cancelledByEmployee.fullName
                                            }
                                            label="Cancelled By"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={
                                                worksOrder.dateCancelled &&
                                                moment(worksOrder.dateCancelled).format(
                                                    'DD-MMM-YYYY'
                                                )
                                            }
                                            label="Date Cancelled"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            required={editing()}
                                            value={worksOrder.reasonCancelled}
                                            label="Reason Cancelled"
                                            helperText={
                                                editing()
                                                    ? 'Reason is required if cancelling a works order'
                                                    : ''
                                            }
                                            propertyName="reasonCancelled"
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={worksOrder.kittedShort}
                                            label="Kitted Short"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                </Fragment>
                            )}
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || !(createValid() || updateValid())}
                                    saveClick={handleSaveClick}
                                    cancelClick={handleCancelClick}
                                    backClick={handleBackClick}
                                />
                            </Grid>
                        </Fragment>
                    )
                )}
            </Grid>
        </Page>
    );
}

WorksOrder.propTypes = {
    item: PropTypes.shape({}),
    worksOrderDetails: PropTypes.shape({}),
    editStatus: PropTypes.string.isRequired,
    worksOrderDetailsError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    worksOrderError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    snackbarVisible: PropTypes.bool,
    loading: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    fetchWorksOrderDetails: PropTypes.func.isRequired,
    setEditStatus: PropTypes.func.isRequired,
    addItem: PropTypes.func.isRequired,
    updateItem: PropTypes.func.isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    fetchWorksOrder: PropTypes.func.isRequired,
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
    employees: PropTypes.arrayOf(PropTypes.shape({})),
    employeesLoading: PropTypes.bool,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    partsSearchLoading: PropTypes.bool
};

WorksOrder.defaultProps = {
    item: {},
    worksOrderDetails: null,
    worksOrderDetailsError: null,
    worksOrderError: null,
    snackbarVisible: false,
    loading: false,
    employees: null,
    employeesLoading: false,
    partsSearchResults: null,
    partsSearchLoading: false
};

export default WorksOrder;
