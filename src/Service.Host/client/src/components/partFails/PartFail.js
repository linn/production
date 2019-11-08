import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import CloseIcon from '@material-ui/icons/Close';
import Tooltip from '@material-ui/core/Tooltip';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
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
    partsSearchResults,
    searchParts,
    partsSearchLoading,
    clearPartsSearch,
    addItem,
    updateItem,
    itemId,
    history,
    errorTypesLoading,
    faultCodesLoading,
    storagePlacesSearchResults,
    searchStoragePlaces,
    storagePlacesSearchLoading,
    clearStoragePlacesSearch,
    clearPartFailErrors
}) {
    const [partFail, setPartFail] = useState({
        dateCreated: new Date().toISOString()
    });

    const [prevPartFail, setPrevPartFail] = useState({});

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';
    const inputInvalid = () => !partFail.partNumber || !partFail.faultCode || !partFail.errorType;

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
        } else {
            setPartFail(a => ({
                ...a,
                faultDescription: ''
            }));
        }
    }, [faultCodes, partFail.faultCode]);

    useEffect(() => {
        clearPartFailErrors();
    }, []);

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPartFail({ ...partFail, [propertyName]: newValue });
    };

    const handleSaveClick = () => {
        clearPartFailErrors();
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
            marginLeft: theme.spacing(-2)
        },
        closeButton: {
            height: theme.spacing(4.5),
            marginTop: theme.spacing(3)
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
                            <ErrorCard
                                errorMessage={`${itemError.item} ${itemError.statusText} - ${
                                    itemError.details?.errors?.[0]
                                }`}
                            />
                        </Grid>
                    ))}
                {loading || errorTypesLoading || faultCodesLoading ? (
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
                                    <Grid item xs={10} />
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
                                <Grid item xs={3}>
                                    <DatePicker
                                        value={partFail.dateCreated}
                                        label="Date Created"
                                        disabled
                                    />
                                </Grid>
                                <Grid item xs={6} />

                                <Grid item xs={5}>
                                    <InputField
                                        label="Part (click search icon to change)"
                                        maxLength={14}
                                        fullWidth
                                        value={partFail.partNumber}
                                        onChange={() => {}}
                                        propertyName="partNumber"
                                        required
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
                                {partFail.partNumber && (
                                    <Fragment>
                                        <Grid item xs={6} />
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
                                        <Grid item xs={6} />

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
                                                items={faultCodes.map(c => c.faultCode)}
                                                fullWidth
                                                value={partFail.faultCode}
                                                allowNoValue={false}
                                                onChange={handleFieldChange}
                                                required
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
                                                items={errorTypes.map(c => c.errorType)}
                                                fullWidth
                                                value={partFail.errorType}
                                                allowNoValue={false}
                                                onChange={handleFieldChange}
                                                required
                                            />
                                        </Grid>
                                        <Grid item xs={9} />
                                        <Grid item xs={6}>
                                            <InputField
                                                fullWidth
                                                rows={4}
                                                value={partFail.story}
                                                label="Story"
                                                onChange={handleFieldChange}
                                                propertyName="story"
                                            />
                                        </Grid>
                                        <Grid item xs={6} />

                                        <Grid item xs={5}>
                                            <InputField
                                                label="Works Order"
                                                maxLength={14}
                                                fullWidth
                                                value={partFail.worksOrderNumber}
                                                onChange={handleFieldChange}
                                                propertyName="worksOrderNumber"
                                                type="number"
                                            />
                                        </Grid>
                                        <Grid item xs={1}>
                                            <div className={classes.marginTop}>
                                                <TypeaheadDialog
                                                    title="Search For Works Order"
                                                    onSelect={newValue => {
                                                        setEditStatus('edit');
                                                        setPartFail(a => ({
                                                            ...a,
                                                            worksOrderNumber: newValue.name
                                                        }));
                                                    }}
                                                    searchItems={worksOrdersSearchResults.map(
                                                        w => ({
                                                            name: w.orderNumber,
                                                            description: w.partNumber
                                                        })
                                                    )}
                                                    loading={worksOrdersSearchLoading}
                                                    fetchItems={searchWorksOrders}
                                                    clearSearch={clearWorksOrdersSearch}
                                                />
                                            </div>
                                        </Grid>
                                        <Grid item xs={5} />
                                        <Grid item xs={5}>
                                            <InputField
                                                label="Purchase Order"
                                                maxLength={14}
                                                fullWidth
                                                value={partFail.purchaseOrderNumber}
                                                onChange={handleFieldChange}
                                                propertyName="purchaseOrderNumber"
                                                type="number"
                                            />
                                        </Grid>
                                        <Grid item xs={1}>
                                            <div className={classes.marginTop}>
                                                <TypeaheadDialog
                                                    title={`Search For a ${partFail.partNumber} Purchase Order`}
                                                    onSelect={newValue => {
                                                        setEditStatus('edit');
                                                        setPartFail(a => ({
                                                            ...a,
                                                            purchaseOrderNumber: newValue.name
                                                        }));
                                                    }}
                                                    searchItems={purchaseOrdersSearchResults
                                                        .filter(d =>
                                                            d.parts?.includes(partFail.partNumber)
                                                        )
                                                        .map(w => ({
                                                            name: w.orderNumber,
                                                            description: w.parts[0]
                                                        }))}
                                                    loading={purchaseOrdersSearchLoading}
                                                    fetchItems={searchPurchaseOrders}
                                                    clearSearch={clearPurchaseOrdersSearch}
                                                />
                                            </div>
                                        </Grid>
                                        <Grid item xs={5} />
                                        <Grid item xs={4}>
                                            <InputField
                                                label="Storage Place (click search icon to change)"
                                                fullWidth
                                                value={partFail.storagePlace}
                                                onChange={handleFieldChange}
                                                propertyName="storagePlace"
                                            />
                                        </Grid>
                                        <Grid item xs={1}>
                                            <div className={classes.marginTop}>
                                                <TypeaheadDialog
                                                    title="Search For a Storage Place"
                                                    onSelect={newValue => {
                                                        setEditStatus('edit');
                                                        setPartFail(a => ({
                                                            ...a,
                                                            storagePlace: newValue.name,
                                                            storagePlaceDescription:
                                                                newValue.description
                                                        }));
                                                    }}
                                                    searchItems={storagePlacesSearchResults.map(
                                                        w => ({
                                                            name: w.storagePlaceId,
                                                            description: w.description
                                                        })
                                                    )}
                                                    loading={storagePlacesSearchLoading}
                                                    fetchItems={searchStoragePlaces}
                                                    clearSearch={clearStoragePlacesSearch}
                                                />
                                            </div>
                                        </Grid>
                                        <Grid item xs={1}>
                                            <Tooltip title="Clear">
                                                <span>
                                                    <Button
                                                        color="primary"
                                                        aria-label="Clear"
                                                        disabled={!partFail.storagePlace}
                                                        onClick={() => {
                                                            setPartFail(a => ({
                                                                ...a,
                                                                storagePlace: '',
                                                                storagePlaceDescription: ''
                                                            }));
                                                        }}
                                                        variant="outlined"
                                                        className={classes.closeButton}
                                                    >
                                                        <CloseIcon />
                                                    </Button>
                                                </span>
                                            </Tooltip>
                                        </Grid>
                                        <Grid item xs={6}>
                                            <InputField
                                                fullWidth
                                                value={partFail.storagePlaceDescription}
                                                label="Description"
                                                onChange={() => {}}
                                                propertyName="storagePlaceDescription"
                                            />
                                        </Grid>
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
                                )}
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
    errorTypesLoading: PropTypes.bool,
    faultCodesLoading: PropTypes.bool,
    clearPartsSearch: PropTypes.func.isRequired,
    storagePlacesSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    searchStoragePlaces: PropTypes.func.isRequired,
    storagePlacesSearchLoading: PropTypes.bool,
    clearStoragePlacesSearch: PropTypes.func.isRequired,
    clearPartFailErrors: PropTypes.func.isRequired
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
    worksOrdersSearchResults: [],
    worksOrdersSearchLoading: false,
    purchaseOrdersSearchResults: [],
    purchaseOrdersSearchLoading: false,
    partsSearchResults: [],
    errorTypesLoading: false,
    faultCodesLoading: false,
    storagePlacesSearchResults: [],
    storagePlacesSearchLoading: false,
    partsSearchLoading: false
};

export default PartFail;
