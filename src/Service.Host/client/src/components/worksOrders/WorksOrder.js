import React, { Fragment, useState, useEffect, useCallback } from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import {
    Title,
    ErrorCard,
    Loading,
    SnackbarMessage,
    InputField,
    SearchInputField,
    CreateButton,
    SaveBackCancelButtons,
    useSearch
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

// TODO conditionals around small function components?

function WorksOrder({
    item,
    editStatus,
    errorMessage,
    history,
    loading,
    snackbarVisible,
    worksOrderDetails,
    departments,
    departmentsLoading,
    employees,
    addItem,
    updateItem,
    setSnackbarVisible,
    fetchWorksOrderDetails,
    setEditStatus,
    fetchWorksOrder
}) {
    const [worksOrder, setWorksOrder] = useState({});
    const [prevWorksOrder, setPrevWorksOrder] = useState({});
    const [selectedDepartment, setSelectedDepartment] = useState(null);
    const [raisedByEmployee, setRaisedByEmployee] = useState(null);
    const [cancelledByEmployee, setCancelledByEmployee] = useState(null);
    const [searchTerm, setSearchTerm] = useState(null);

    useSearch(fetchWorksOrder, searchTerm, null);

    const creating = useCallback(() => editStatus === 'create', [editStatus]);
    // const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';

    // TODO is this going to render every time?
    useEffect(() => {
        if (item !== prevWorksOrder) {
            setWorksOrder(item);
            setPrevWorksOrder(item);
            if (item && item.partNumber) {
                fetchWorksOrderDetails(item.partNumber);
            }
        }
    }, [item, prevWorksOrder, fetchWorksOrderDetails]);

    useEffect(() => {
        if (departments && worksOrder) {
            setSelectedDepartment(
                departments.find(
                    department => department.departmentCode === worksOrder.raisedByDepartment
                )
            );
        }
    }, [departments, worksOrder]);

    // TODO could this be in the same effect above?
    useEffect(() => {
        if (employees && worksOrder) {
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
                workStationCode: worksOrderDetails.worksStationCode
            }));
        }
    }, [creating, worksOrderDetails]);

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
        history.push('/production/works-orders');
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
                                onClick={() => history.push('/production/works-orders')}
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
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
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
                {/* TODO check more loadings? */}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    (worksOrder || creating()) && (
                        // worksOrder && (
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
                                    value={creating() ? 'WO' : worksOrder.docType}
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
                                            label="Type"
                                        />
                                    </Grid>
                                    <Grid item xs={8} />
                                </Fragment>
                            )}
                            {/* TODO search */}
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={worksOrder.partNumber}
                                    label="Part Number"
                                    propertyName="partNumber"
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={8}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={
                                        worksOrderDetails ? worksOrderDetails.partDescription : ''
                                    }
                                    // rows={2}
                                    label="Description"
                                />
                            </Grid>
                            {/* <Grid item xs={4} /> */}
                            <Grid item xs={4}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={worksOrder.workStationCode}
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
                                    helperText="Required"
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
                                    value={selectedDepartment && selectedDepartment.description}
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
                                            helperText="Reason is required if cancelling a works order"
                                            propertyName="reasonCancelled"
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
                                    saveDisabled={viewing()}
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
    errorMessage: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    loading: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    fetchWorksOrderDetails: PropTypes.func.isRequired,
    setEditStatus: PropTypes.func.isRequired,
    addItem: PropTypes.func.isRequired,
    updateItem: PropTypes.func.isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    fetchWorksOrder: PropTypes.func.isRequired
};

WorksOrder.defaultProps = {
    item: null,
    worksOrderDetails: null,
    errorMessage: '',
    snackbarVisible: false,
    loading: false
};

export default WorksOrder;
