import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import FormControl from '@material-ui/core/FormControl';
import MenuItem from '@material-ui/core/MenuItem';
import InputLabel from '@material-ui/core/InputLabel';
import Select from '@material-ui/core/Select';
import {
    CheckboxWithLabel,
    InputField,
    Loading,
    Title,
    ErrorCard,
    Dropdown,
    SnackbarMessage,
    TypeaheadDialog,
    Typeahead,
    DatePicker,
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
    clearPartFailErrors,
    employees,
    employeesLoading
}) {
    const [partFail, setPartFail] = useState({
        dateCreated: new Date().toISOString()
    });

    const [searchResults, setSearchResults] = useState([]);

    useEffect(() => {
        setSearchResults(employees);
    }, [employees]);

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
                                                setSearchResults(
                                                    employees?.filter(i =>
                                                        i.fullName?.includes(
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
                                                step="0.5"
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
                                        <Grid item xs={6}>
                                            <CheckboxWithLabel
                                                label="No Cost?"
                                                checked={partFail.noCost}
                                                onChange={() =>
                                                    handleFieldChange('noCost', !partFail.noCost)
                                                }
                                            />
                                        </Grid>
                                        <Grid item xs={3}>
                                            <FormControl variant="standard" fullWidth>
                                                <InputLabel>Fault Code</InputLabel>
                                                <Select
                                                    value={partFail.faultCode}
                                                    label="Fault Code"
                                                    propertyName="faultCode"
                                                    allowNoValue={creating()}
                                                    onChange={e =>
                                                        handleFieldChange(
                                                            'faultCode',
                                                            e.target.value
                                                        )
                                                    }
                                                    required
                                                >
                                                    {/* display valid options at the top so we don't need to scroll through old ones */}
                                                    {faultCodes.map(e => {
                                                        return e.dateInvalid === null ? (
                                                            <MenuItem value={e.faultCode}>
                                                                {e.faultCode} - {e.faultDescription}
                                                            </MenuItem>
                                                        ) : null;
                                                    })}
                                                    {faultCodes.map(e => {
                                                        return e.dateInvalid !== null ? (
                                                            <MenuItem value={e.faultCode} disabled>
                                                                {e.faultCode} - {e.faultDescription}
                                                            </MenuItem>
                                                        ) : null;
                                                    })}
                                                </Select>
                                            </FormControl>
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
                                            <FormControl variant="standard" fullWidth>
                                                <InputLabel>Error Type</InputLabel>
                                                <Select
                                                    value={partFail.errorType}
                                                    label="Error Type"
                                                    propertyName="errorType"
                                                    allowNoValue={creating()}
                                                    onChange={e =>
                                                        handleFieldChange(
                                                            'errorType',
                                                            e.target.value
                                                        )
                                                    }
                                                    required
                                                >
                                                    {/* display valid options at the top so we don't need to scroll through old ones */}
                                                    {errorTypes.map(e => {
                                                        return e.dateInvalid === null ? (
                                                            <MenuItem value={e.errorType}>
                                                                {e.errorType}
                                                            </MenuItem>
                                                        ) : null;
                                                    })}
                                                    {errorTypes.map(e => {
                                                        return e.dateInvalid !== null ? (
                                                            <MenuItem value={e.errorType} disabled>
                                                                {e.errorType}
                                                            </MenuItem>
                                                        ) : null;
                                                    })}
                                                </Select>
                                            </FormControl>
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
                                        <Grid item xs={3}>
                                            <Dropdown
                                                label="Sentence Decision"
                                                propertyName="sentenceDecision"
                                                items={['SCRAP', 'REPROCESS', 'CONCESSION']}
                                                fullWidth
                                                value={
                                                    partFail.sentenceDecision
                                                        ? partFail.sentenceDecision
                                                        : ''
                                                }
                                                allowNoValue
                                                onChange={handleFieldChange}
                                            />
                                        </Grid>
                                        <Grid item xs={4}>
                                            <DatePicker
                                                label="Date Sentenced"
                                                value={
                                                    partFail.dateSentenced
                                                        ? partFail.dateSentenced.toString()
                                                        : null
                                                }
                                                onChange={value => {
                                                    handleFieldChange('dateSentenced', value);
                                                }}
                                                disabled={!partFail.sentenceDecision}
                                            />
                                        </Grid>
                                        <Grid item xs={5} />
                                        <Grid item xs={6}>
                                            <InputField
                                                fullWidth
                                                rows={4}
                                                value={partFail.sentenceReason}
                                                label="Sentence Reason"
                                                onChange={handleFieldChange}
                                                propertyName="sentenceReason"
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
                                        <Grid item xs={9} />
                                        <Grid item xs={3}>
                                            {employeesLoading ? (
                                                <Loading />
                                            ) : (
                                                <Typeahead
                                                    items={searchResults.slice(0, 10)}
                                                    fetchItems={searchTerm => {
                                                        setSearchResults(
                                                            employees?.filter(i =>
                                                                i.fullName?.includes(
                                                                    searchTerm?.toUpperCase()
                                                                )
                                                            )
                                                        );
                                                    }}
                                                    links={false}
                                                    modal
                                                    value={partFail.owner}
                                                    onSelect={newValue => {
                                                        setPartFail(f => ({
                                                            ...f,
                                                            owner: newValue.name,
                                                            ownerName: newValue.description
                                                        }));
                                                        setEditStatus('edit');
                                                    }}
                                                    label="Owner"
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
                                                value={partFail.ownerName}
                                                label="Name"
                                                disabled
                                                onChange={() => {}}
                                                propertyName="ownerName"
                                            />
                                        </Grid>
                                        <Grid item xs={3} />
                                        <Grid item xs={6}>
                                            <InputField
                                                fullWidth
                                                rows={4}
                                                value={partFail.comments}
                                                label="Comments"
                                                onChange={handleFieldChange}
                                                propertyName="comments"
                                            />
                                        </Grid>
                                        <Grid item xs={6} />
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
    partsSearchLoading: false,
    employees: [],
    employeesLoading: false
};

export default PartFail;
