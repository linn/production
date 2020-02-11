import React, { Fragment, useState, useEffect, useRef } from 'react';
import moment from 'moment';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    SaveBackCancelButtons
} from '@linn-it/linn-form-components-library';
import Page from '../containers/Page';
import PurchaseOrderLine from './PurchaseOrderLine';

function usePrevious(value) {
    const ref = useRef();
    useEffect(() => {
        ref.current = value;
    });
    return ref.current;
}

function PurchaseOrder({
    editStatus,
    itemError,
    itemId,
    item,
    snackbarVisible,
    setEditStatus,
    setSnackbarVisible,
    itemLoading,
    issueSernos,
    buildSernos,
    buildError,
    issueError,
    history,
    updatePurchaseOrder,
    issueWorking,
    buildWorking,
    initialise,
    issueSernosSnackbarVisible,
    buildSernosSnackbarVisible,
    issueMessage,
    buildMessage,
    setIssueMessageVisible,
    setBuildMessageVisible
}) {
    const [purchaseOrder, setPurchaseOrder] = useState({});
    const [prevPurchaseOrder, setPrevpurchaseOrder] = useState({});

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

    const prevProps = usePrevious({ issueWorking, buildWorking });

    useEffect(() => {
        // if process loading state changes and process is complete
        if (
            (prevProps?.issueWorking !== issueWorking && issueWorking === false) ||
            (prevProps?.buildWorking !== buildWorking && !buildWorking === false)
        ) {
            // reload the purchase order
            initialise({ itemId });
        }
    }, [issueWorking, buildWorking, initialise, itemId, prevProps]);

    const handleSaveClick = () => updatePurchaseOrder(itemId, purchaseOrder);
    const handleCancelClick = () => {
        setEditStatus('view');
        setPurchaseOrder(item);
    };

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
            <Page>
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
        <Page showRequestErrors={false}>
            <Grid container spacing={3}>
                {itemError ? (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError?.statusText} />
                    </Grid>
                ) : (
                    <Fragment>
                        {issueError && (
                            <Grid item xs={12}>
                                <ErrorCard
                                    errorMessage={`${issueError?.statusText} - ${issueError?.item}`}
                                />
                            </Grid>
                        )}
                        {buildError && (
                            <Grid item xs={12}>
                                <ErrorCard
                                    errorMessage={`${buildError?.statusText} - ${buildError?.item}`}
                                />
                            </Grid>
                        )}
                        {purchaseOrder && (
                            <Fragment>
                                <SnackbarMessage
                                    visible={snackbarVisible}
                                    onClose={() => setSnackbarVisible(false)}
                                    message="Save Successful"
                                />
                                <SnackbarMessage
                                    visible={issueSernosSnackbarVisible}
                                    onClose={() => setIssueMessageVisible(false)}
                                    message={issueMessage}
                                />
                                <SnackbarMessage
                                    visible={buildSernosSnackbarVisible}
                                    onClose={() => setBuildMessageVisible(false)}
                                    message={buildMessage}
                                />
                                <Grid item xs={12}>
                                    <Title text={`Purchase Order ${purchaseOrder.orderNumber}`} />
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
                                <Grid item xs={8} />
                                <Grid item xs={6}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        rows={7}
                                        value={formatAddress()}
                                        label="Supplier"
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <InputField
                                        fullWidth
                                        rows={6}
                                        value={purchaseOrder.remarks}
                                        onChange={handleFieldChange}
                                        propertyName="remarks"
                                        label="Remarks"
                                    />
                                </Grid>
                                {purchaseOrder.detailSernosInfos?.map(d => (
                                    <PurchaseOrderLine
                                        key={d.orderLine}
                                        detail={d}
                                        partNumber={d.partNumber}
                                        orderNumber={purchaseOrder.orderNumber}
                                        issueSernos={issueSernos}
                                        buildSernos={buildSernos}
                                    />
                                ))}
                            </Fragment>
                        )}
                        <Grid item xs={12}>
                            <SaveBackCancelButtons
                                saveDisabled={viewing()}
                                saveClick={handleSaveClick}
                                cancelClick={handleCancelClick}
                                backClick={() => {
                                    history.push('/production/resources');
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
    itemLoading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    itemError: PropTypes.shape({}),
    setSnackbarVisible: PropTypes.func.isRequired,
    issueSernos: PropTypes.func.isRequired,
    buildSernos: PropTypes.func.isRequired,
    updatePurchaseOrder: PropTypes.func.isRequired,
    issueError: PropTypes.shape({}),
    buildError: PropTypes.shape({}),
    issueSernosSnackbarVisible: PropTypes.bool,
    buildSernosSnackbarVisible: PropTypes.bool,
    issueMessage: PropTypes.string,
    buildMessage: PropTypes.string,
    setIssueMessageVisible: PropTypes.func.isRequired,
    setBuildMessageVisible: PropTypes.func.isRequired,
    issueWorking: PropTypes.bool,
    buildWorking: PropTypes.bool,
    initialise: PropTypes.func.isRequired
};

PurchaseOrder.defaultProps = {
    item: {},
    snackbarVisible: false,
    itemLoading: false,
    itemId: null,
    itemError: null,
    issueError: null,
    buildError: null,
    issueSernosSnackbarVisible: false,
    buildSernosSnackbarVisible: false,
    issueMessage: null,
    buildMessage: null,
    issueWorking: null,
    buildWorking: null
};

export default PurchaseOrder;
