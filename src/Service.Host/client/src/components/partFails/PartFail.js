import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    ValidatedInputDialog,
    TypeaheadDialog,
    Dropdown,
    SaveBackCancelButtons,
    DatePicker
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';

function PartFail({
    editStatus,
    itemErrors,
    item,
    loading,
    snackbarVisible,
    setEditStatus,
    setSnackbarVisible,
    profile,
    worksOrdersSearchResults,
    worksOrdersSearchLoading,
    searchWorksOrders,
    clearWorksOrdersSearch,
    purchaseOrdersSearchResults,
    purchaseOrdersSearchLoading,
    searchPurchaseOrders,
    clearPurchaseOrdersSearch,
    faultCodes,
    errorTypes,
    storagePlaces,
    partsSearchResults,
    searchParts,
    partsSearchLoading,
    clearPartsSearch,
    addItem,
    updateItem,
    itemId,
    history
}) {
    const [partFail, setPartFail] = useState({
        dateCreated: new Date().toISOString()
    });

    const [prevPartFail, setPrevPartFail] = useState({});

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';
    const inputInvalid = () => false;

    const errorTypeOptions = () =>
        errorTypes.length > 0 && partFail.errorType
            ? errorTypes.filter(t => t.dateInvalid == null)?.map(p => p.errorType)
            : ['loading...'];
    const errorTypeValue = () =>
        errorTypes.length > 0
            ? errorTypes.find(p => p.errorType === partFail.errorType)?.errorType
            : 'loading...';

    const faultCodeOptions = () =>
        faultCodes.length > 0 && partFail.faultCode
            ? faultCodes.map(p => p.faultCode)
            : ['loading...'];
    const faultCodeValue = () =>
        faultCodes.length > 0
            ? faultCodes.find(p => p.faultCode === partFail.faultCode)?.faultCode
            : 'loading...';

    const storagePlaceOptions = () =>
        storagePlaces.length > 0 && partFail.storagePlace
            ? storagePlaces.map(p => p.storagePlaceId)
            : ['loading...'];
    const storagePlaceValue = () =>
        storagePlaces.length > 0
            ? storagePlaces.find(p => p.storagePlaceId === partFail.storagePlace)?.storagePlaceId
            : 'loading...';

    useEffect(() => {
        if (editStatus !== 'create' && item && item !== prevPartFail) {
            setPartFail(item);
            setPrevPartFail(item);
        }
    }, [item, prevPartFail, editStatus]);

    useEffect(() => {
        if (editStatus === 'create' && profile) {
            setPartFail(a => ({
                ...a,
                enteredBy: profile.employee.replace('/employees/', ''), // the current user
                enteredByName: profile.name
            }));
        }
    }, [profile, editStatus]);

    useEffect(() => {
        if (faultCodes && partFail.faultCode) {
            setPartFail(a => ({
                ...a,
                faultDescription: faultCodes.find(c => c.faultCode === a.faultCode)
                    ?.faultDescription
            }));
        }
    }, [faultCodes, partFail.faultCode]);

    useEffect(() => {
        if (storagePlaces && partFail.storagePlace) {
            setPartFail(a => ({
                ...a,
                storagePlaceDescription: storagePlaces.find(
                    c => c.storagePlaceId === a.storagePlace
                )?.description
            }));
        }
    }, [storagePlaces, partFail.storagePlace]);

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPartFail({ ...partFail, [propertyName]: newValue });
    };

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, partFail);
            setEditStatus('view');
        } else if (creating()) {
            addItem(partFail);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        if (creating()) {
            setPartFail({
                enteredBy: profile.employee.replace('/employees/', ''),
                enteredByName: profile.name
            });
        } else {
            setPartFail(item);
        }
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/quality/part-fails');
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
                        <Title text="Log A Part Fail" />
                    ) : (
                        <Title text="Part Fail Details" />
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
                    partFail.id !== '' &&
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
                                            value={partFail.id}
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
                                <Grid item xs={3}>
                                    <InputField
                                        fullWidth
                                        disabled
                                        value={partFail.enteredByName}
                                        label="Entered By"
                                        maxLength={10}
                                        onChange={handleFieldChange}
                                        propertyName="enteredByName"
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <DatePicker
                                        value={partFail.dateCreated}
                                        label="Date Created"
                                        disabled
                                    />
                                </Grid>
                                {creating() && <Grid item xs={3} />}

                                <Grid item xs={5}>
                                    <InputField
                                        label="Part (click search button to Search for a Part)"
                                        maxLength={14}
                                        fullWidth
                                        value={partFail.partNumber}
                                        onChange={() => {}}
                                        propertyName="partNumber"
                                    />
                                </Grid>
                                <Grid item xs={1}>
                                    <div className={classes.marginTop}>
                                        <TypeaheadDialog
                                            title="Search For Part"
                                            onSelect={newValue => {
                                                setEditStatus('edit');
                                                setPartFail(a => ({
                                                    ...a,
                                                    partNumber: newValue.partNumber,
                                                    partDescription: newValue.description
                                                }));
                                            }}
                                            searchItems={partsSearchResults}
                                            loading={partsSearchLoading}
                                            fetchItems={searchParts}
                                            clearSearch={() => clearPartsSearch}
                                        />
                                    </div>
                                </Grid>
                                <Grid item xs={6}>
                                    <InputField
                                        fullWidth
                                        rows={3}
                                        value={partFail.partDescription}
                                        label="Description"
                                        maxLength={10}
                                        onChange={() => {}}
                                        propertyName="partDescription"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        fullWidth
                                        type="number"
                                        value={partFail.quantity}
                                        label="Quantity"
                                        onChange={handleFieldChange}
                                        propertyName="quantity"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        fullWidth
                                        value={partFail.batch}
                                        label="Batch"
                                        onChange={handleFieldChange}
                                        propertyName="batch"
                                    />
                                </Grid>
                                <Grid item xs={6} />
                                <Grid item xs={3}>
                                    <Dropdown
                                        label="Fault Code"
                                        propertyName="faultCode"
                                        items={faultCodeOptions()}
                                        fullWidth
                                        value={faultCodeValue()}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <InputField
                                        fullWidth
                                        value={partFail.faultDescription}
                                        label="Description"
                                        onChange={() => {}}
                                        propertyName="faultDescription"
                                    />
                                </Grid>
                                <Grid item xs={3} />
                                <Grid item xs={3}>
                                    <Dropdown
                                        label="Error Type"
                                        propertyName="errorType"
                                        items={errorTypeOptions()}
                                        fullWidth
                                        value={errorTypeValue()}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={8}>
                                    <InputField
                                        fullWidth
                                        rows={4}
                                        value={partFail.story}
                                        label="Story"
                                        onChange={handleFieldChange}
                                        propertyName="story"
                                    />
                                </Grid>

                                <Grid item xs={5}>
                                    <InputField
                                        label="Works Order (click edit button to change)"
                                        maxLength={14}
                                        fullWidth
                                        onChange={() => {}}
                                        value={partFail.worksOrderNumber}
                                        propertyName="worksOrderNumber"
                                    />
                                </Grid>
                                <Grid item xs={1}>
                                    <div className={classes.marginTop}>
                                        <ValidatedInputDialog
                                            title="Enter a Valid Works Order"
                                            searchItems={worksOrdersSearchResults}
                                            loading={worksOrdersSearchLoading}
                                            onAccept={accepted => {
                                                setEditStatus('edit');
                                                setPartFail(a => ({
                                                    ...a,
                                                    worksOrderNumber: accepted.orderNumber
                                                }));
                                            }}
                                            fetchItems={searchWorksOrders}
                                            clearSearch={clearWorksOrdersSearch}
                                            searchItemId="orderNumber"
                                        />
                                    </div>
                                </Grid>
                                <Grid item xs={6} />

                                <Grid item xs={5}>
                                    <InputField
                                        label="Purchase Order (click edit button to change)"
                                        maxLength={14}
                                        fullWidth
                                        onChange={() => {}}
                                        value={partFail.purchaseOrderNumber}
                                        propertyName="purchaseOrderNumber"
                                    />
                                </Grid>
                                <Grid item xs={1}>
                                    <div className={classes.marginTop}>
                                        <ValidatedInputDialog
                                            title="Enter a Valid Purchase Order"
                                            searchItems={purchaseOrdersSearchResults}
                                            loading={purchaseOrdersSearchLoading}
                                            onAccept={accepted => {
                                                setEditStatus('edit');
                                                setPartFail(a => ({
                                                    ...a,
                                                    purchaseOrderNumber: accepted.orderNumber
                                                }));
                                            }}
                                            fetchItems={searchPurchaseOrders}
                                            clearSearch={clearPurchaseOrdersSearch}
                                            searchItemId="orderNumber"
                                        />
                                    </div>
                                </Grid>
                                <Grid item xs={6} />
                                <Grid item xs={3}>
                                    <Dropdown
                                        label="Storage Place"
                                        propertyName="storagePlace"
                                        items={storagePlaceOptions()}
                                        fullWidth
                                        value={storagePlaceValue()}
                                        onChange={handleFieldChange}
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        fullWidth
                                        value={partFail.storagePlaceDescription}
                                        label="Storage Place Description"
                                        onChange={() => {}}
                                        propertyName="storagePlaceDescription"
                                    />
                                </Grid>
                                <Grid item xs={6} />
                                <Grid item xs={3}>
                                    <InputField
                                        fullWidth
                                        type="number"
                                        value={partFail.minutesWasted}
                                        label="Minutes Wasted"
                                        onChange={handleFieldChange}
                                        propertyName="minutesWasted"
                                    />
                                </Grid>
                            </Fragment>
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

PartFail.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    profile: PropTypes.shape({}),
    editStatus: PropTypes.string.isRequired,
    snackbarVisible: PropTypes.bool,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    item: PropTypes.shape({}),
    itemErrors: PropTypes.arrayOf(
        PropTypes.shape({
            status: PropTypes.number,
            statusText: PropTypes.string,
            details: PropTypes.shape({}),
            item: PropTypes.string
        })
    ),
    storagePlaces: PropTypes.arrayOf(PropTypes.shape({})),
    faultCodes: PropTypes.arrayOf(PropTypes.shape({})),
    errorTypes: PropTypes.arrayOf(PropTypes.shape({})),
    addItem: PropTypes.func,
    updateItem: PropTypes.func,
    itemId: PropTypes.string,
    worksOrdersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    worksOrdersSearchLoading: PropTypes.bool,
    searchWorksOrders: PropTypes.func.isRequired,
    clearWorksOrdersSearch: PropTypes.func.isRequired,
    purchaseOrdersSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    purchaseOrdersSearchLoading: PropTypes.bool,
    searchPurchaseOrders: PropTypes.func.isRequired,
    clearPurchaseOrdersSearch: PropTypes.func.isRequired,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    searchParts: PropTypes.func.isRequired,
    partsSearchLoading: PropTypes.bool,
    clearPartsSearch: PropTypes.func.isRequired
};

PartFail.defaultProps = {
    snackbarVisible: false,
    loading: null,
    profile: { employee: '', name: '' },
    errorTypes: [],
    item: null,
    itemErrors: [],
    faultCodes: [],
    addItem: null,
    updateItem: null,
    itemId: null,
    storagePlaces: [],
    worksOrdersSearchResults: [],
    worksOrdersSearchLoading: false,
    purchaseOrdersSearchResults: [],
    purchaseOrdersSearchLoading: false,
    partsSearchResults: [],
    partsSearchLoading: false
};

export default PartFail;
