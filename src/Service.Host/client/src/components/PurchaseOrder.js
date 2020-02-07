import React, { Fragment, useState, useEffect } from 'react';
import moment from 'moment';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    SaveBackCancelButtons,
    Dropdown
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import Page from '../containers/Page';

function PurchaseOrder({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    itemLoading,
    snackbarVisible,
    setEditStatus,
    setSnackbarVisible,
    updatePurchaseOrder
}) {
    const [purchaseOrder, setPurchaseOrder] = useState({});
    const [prevPurchaseOrder, setPrevpurchaseOrder] = useState({});
    const [qtyToBuild, setQtyToBuild] = useState();
    const [qtyToIssue, setQtyToIssue] = useState();
    const [from, setFrom] = useState();
    const [to, setTo] = useState();



    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevPurchaseOrder) {
            setPurchaseOrder({ ...item, from: null, To: null, qtyToBuild: null, qtyToIssue: null });
            setPrevpurchaseOrder(item);
        }
    }, [item, prevPurchaseOrder]);

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPurchaseOrder({ ...purchaseOrder, [propertyName]: newValue });
    };

    const useStyles = makeStyles(theme => ({
        marginTop: {
            marginTop: theme.spacing(2),
            marginLeft: theme.spacing(-2)
        },
        closeButton: {
            height: theme.spacing(4.5),
            marginTop: theme.spacing(4.5),
            marginLeft: theme.spacing(-1)
        }
    }));

    const classes = useStyles();

    const handleSaveClick = () => updatePurchaseOrder(itemId, purchaseOrder);
    const handleCancelClick = () => {};

    const formatAddress = () => {
        let address = purchaseOrder.addressee;
        if (purchaseOrder.address1) {
            address += `
${purchaseOrder.address1}`;
        }
        if (purchaseOrder.address2) {
            address += `
${purchaseOrder.address2}`;
        }
        if (purchaseOrder.address3) {
            address += `
${purchaseOrder.address3}`;
        }
        if (purchaseOrder.address4) {
            address += `
${purchaseOrder.address4}`;
        }
        if (purchaseOrder.address4) {
            address += `
${purchaseOrder.address4}`;
        }
        if (purchaseOrder.postCode) {
            address += `
${purchaseOrder.postCode}`;
        }
        if (purchaseOrder.country) {
            address += `
${purchaseOrder.country}`;
        }

        return address;
    };

    if (itemLoading) {
        return (
            <Page showRequestErrors>
                <Grid item xs={12}>
                    <Title text="Purchase Order" />
                </Grid>
                <Grid item xs={12}>
                    <Loading />
                </Grid>
            </Page>
        );
    }

    return (
        <Page showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Title text="Purchase Order" />
                </Grid>
                {itemError ? (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                ) : (
                    <Fragment>
                        {purchaseOrder && (
                            <Fragment>
                                <SnackbarMessage
                                    visible={snackbarVisible}
                                    onClose={() => setSnackbarVisible(false)}
                                    message="Save Successful"
                                />

                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        label="Order Number"
                                        value={purchaseOrder.orderNumber}
                                        propertyName="orderNumber"
                                        onChange={() => {}}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={
                                            purchaseOrder.dateOfOrder
                                                ? moment(purchaseOrder.dateOfOrder).format(
                                                      'DD-MMM-YYYY'
                                                  )
                                                : null
                                        }
                                        label="Date"
                                    />
                                </Grid>
                                <Grid item xs={4} />
                                <Grid item xs={6}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        rows={7}
                                        value={formatAddress()}
                                        label="Supplier"
                                    />
                                </Grid>
                                <Grid item xs={6} />
                                <Grid item xs={6}>
                                    <InputField
                                        fullWidth
                                        rows={5}
                                        value={purchaseOrder.remarks}
                                        onChange={handleFieldChange}
                                        propertyName="remarks"
                                        label="Remarks"
                                    />
                                </Grid>
                                <Grid item xs={6} />
                                {purchaseOrder.detailSernosInfos?.map(d => (
                                    <Fragment key={d.orderLine}>
                                        <Grid item xs={1}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.orderLine}
                                                label="Line"
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.partNumber}
                                                label="Part"
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.partDescription}
                                                label="Description"
                                            />
                                        </Grid>
                                        <Grid item xs={5} />
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.orderQuantity}
                                                label="Quantity Ordered"
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.ourUnitOfMeasure}
                                                label="Units"
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <Dropdown
                                                fullWidth
                                                items={['', 'Y', 'N']}
                                                label="Issued"
                                                value={d.issuedSerialNumbers}
                                                propertyName="issuedSerialNumbers"
                                                allowNoValue
                                            />
                                        </Grid>
                                        <Grid item xs={3} />
                                        <Grid item xs={1}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.quantityReceived}
                                                label="Received"
                                            />
                                        </Grid>
                                        <Grid item xs={11} />
                                        <Grid item xs={1}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.sernosIssued}
                                                label="Issued"
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.firstSernos}
                                                label="First Serial"
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.lastSernos}
                                                label="Last Serial"
                                            />
                                        </Grid>
                                        <Grid item xs={5} />
                                        <Grid item xs={1}>
                                            <InputField
                                                fullWidth
                                                disabled
                                                value={d.sernosBuilt}
                                                label="Built"
                                            />
                                        </Grid>
                                        <Grid item xs={11} />
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                type="number"
                                                propertyName="qtyToIssue"
                                                onChange={handleFieldChange}
                                                value={purchaseOrder.qtyToIssue}
                                                label="Qty to Issue"
                                            />
                                        </Grid>
                                        <Grid item xs={8} />
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                type="number"
                                                propertyName="qtyToBuild"
                                                onChange={handleFieldChange}
                                                value={purchaseOrder.qtyToBuild}
                                                label="Qty to Build"
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                type="number"
                                                value={purchaseOrder.from}
                                                label="From Serial"
                                                propertyName="from"
                                                onChange={handleFieldChange}
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                type="number"
                                                value={purchaseOrder.to}
                                                label="To Serial"
                                                propertyName="to"
                                                onChange={handleFieldChange}
                                            />
                                        </Grid>
                                        <Grid item xs={3} />
                                        <Grid item xs={12}>
                                            <Button
                                                //className={classes.printButton}
                                                onClick={() => {}}
                                                variant="outlined"
                                                color="primary"
                                            >
                                                Build
                                            </Button>
                                            <Button
                                                onClick={() => {}}
                                                variant="outlined"
                                                color="primary"
                                            >
                                                Issue
                                            </Button>
                                        </Grid>
                                    </Fragment>
                                ))}
                            </Fragment>
                        )}
                        <Grid item xs={12}>
                            <SaveBackCancelButtons
                                saveDisabled={viewing()}
                                saveClick={handleSaveClick}
                                cancelClick={handleCancelClick}
                                backClick={() => {
                                    // history.push(previousPath);
                                }}
                            />
                        </Grid>
                    </Fragment>
                )}
            </Grid>
        </Page>
    );
}

PurchaseOrder.propTypes = {
    item: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    itemError: PropTypes.shape({}),
    setSnackbarVisible: PropTypes.func.isRequired
};

PurchaseOrder.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: null,
    itemId: null,
    itemError: null
};

export default PurchaseOrder;
