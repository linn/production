import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    TypeaheadDialog,
    Dropdown,
    Typeahead,
    SaveBackCancelButtons,
    DateTimePicker
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
    clearPartFailErrors,
    employees,
    employeesLoading
}) {
    const [partFail, setPartFail] = useState({
        dateCreated: new Date().toISOString()
    });

    const [searchResults, setSearchResults] = useState([]);
    const [hasSearched, setHasSearched] = useState(false);

    useEffect(() => {
        if (hasSearched) {
            setSearchResults(employees);
        }
    }, [employees, hasSearched]);

    const [prevPartFail, setPrevPartFail] = useState({});

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';
    const inputInvalid = () =>
        !partFail.partNumber || !partFail.faultCode || !partFail.errorType || !partFail.enteredBy;

    useEffect(() => {
        if (editStatus !== 'create' && item && item !== prevPartFail) {
            setPartFail(item);
            setPrevPartFail(item);
        }
    }, [item, prevPartFail, editStatus]);

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
    }, [clearPartFailErrors]);

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
            const body = partFail;
            body.dateCreated = new Date().toISOString();
            addItem(body);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        if (creating()) {
            setPartFail({
                enteredBy: profile?.employee.replace('/employees/', ''),
                enteredByName: profile?.name
            });
        } else {
            setPartFail(item);
        }
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.goBack();
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
                                errorMessage={`${itemError.item} ${itemError.statusText} - ${itemError.details?.errors?.[0]}`}
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
                                            value={partFail.id}
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
                                <Grid item xs={3}>
                                    {employeesLoading ? (
                                        <Loading />
                                    ) : (
                                        <Typeahead
                                            items={searchResults.slice(0, 10)}
                                            fetchItems={searchTerm => {
                                                setHasSearched(true);
                                                setSearchResults(
                                                    employees?.filter(i =>
                                                        i.fullName.includes(
                                                            searchTerm?.toUpperCase()
                                                        )
                                                    )
                                                );
                                            }}
                                            links={false}
                                            modal
                                            value={partFail.enteredBy}
                                            onSelect={newValue =>
                                                setPartFail(f => ({
                                                    ...f,
                                                    enteredBy: newValue.name,
                                                    enteredByName: newValue.description
                                                }))
                                            }
                                            label="Entered By"
                                            disabled={!creating()}
                                            clearSearch={() => {}}
                                            required
                                            loading={false}
                                            title="Search by Name"
                                            history={history}
                                            minimumSearchTermLength={2}
                                            debounce={1}
                                        />
                                    )}
                                </Grid>
                                <Grid item xs={3}>
                                    <InputField
                                        fullWidth
                                        value={partFail.enteredByName}
                                        label="Name"
                                        disabled
                                        onChange={() => {}}
                                        propertyName="enteredByName"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <DateTimePicker
                                        value={partFail.dateCreated}
                                        label="Date Created"
                                        disabled
                                    />
                                </Grid>
                                <Grid item xs={3} />
                                <Grid item xs={3}>
                                    <Typeahead
                                        onSelect={newValue => {
                                            setEditStatus('edit');
                                            setPartFail(a => ({
                                                ...a,
                                                partNumber: newValue.partNumber,
                                                partDescription: newValue.description
                                            }));
                                        }}
                                        label="Part"
                                        modal
                                        items={partsSearchResults}
                                        value={partFail.partNumber}
                                        loading={partsSearchLoading}
                                        fetchItems={searchParts}
                                        links={false}
                                        clearSearch={() => clearPartsSearch}
                                        placeholder="Search By Part Number"
                                    />
                                </Grid>
                                {partFail.partNumber && (
                                    <>
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
                                        <Grid item xs={3} />
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
                                                allowNoValue={creating()}
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
                                                allowNoValue={creating()}
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
                                        <Grid item xs={5}>
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
                                        <Grid item xs={3}>
                                            <InputField
                                                fullWidth
                                                type="number"
                                                value={partFail.serialNumber}
                                                label="Serial Number"
                                                onChange={handleFieldChange}
                                                propertyName="serialNumber"
                                            />
                                        </Grid>
                                    </>
                                )}
                            </>
                        </>
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
    history: PropTypes.shape({ goBack: PropTypes.func }).isRequired,
    profile: PropTypes.shape({ employee: PropTypes.string, name: PropTypes.string }),
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
    clearPartFailErrors: PropTypes.func.isRequired,
    employees: PropTypes.arrayOf(PropTypes.shape({})),
    employeesLoading: PropTypes.bool
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
    partsSearchLoading: false,
    employees: [],
    employeesLoading: false
};

export default PartFail;
