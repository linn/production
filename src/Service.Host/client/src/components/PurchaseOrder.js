import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    TypeaheadDialog
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import Page from '../containers/Page';

function PurchaseOrder({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    setEditStatus,
    setSnackbarVisible
}) {
    const [purchaseOrder, setPurchaseOrder] = useState({});
    const [prevPurchaseOrder, setPrevpurchaseOrder] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevPurchaseOrder) {
            setPurchaseOrder(item);
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

    if (loading) {
        return (
            <Page showRequestErrors>
                <Grid item xs={12}>
                    <Loading />
                </Grid>
            </Page>
        );
    }

    return (
        <Page showRequestErrors>
            <Grid container spacing={3}>
                {itemError ? (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                ) : (
                    <Fragment>
                        {purchaseOrder && (
                            <Fragment>
                                <Grid item xs={12}>
                                    <Title text="Purchase Order" />
                                </Grid>
                                <SnackbarMessage
                                    visible={snackbarVisible}
                                    onClose={() => setSnackbarVisible(false)}
                                    message="Save Successful"
                                />

                                <Grid item xs={12}></Grid>
                            </Fragment>
                        )}
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
    setSnackbarVisible: PropTypes.func.isRequired,
};

PurchaseOrder.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: null,
    itemId: null,
    itemError: null
};

export default PurchaseOrder;
