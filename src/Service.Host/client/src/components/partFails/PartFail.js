import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import DateTimePicker from '@linn-it/linn-form-components-library/cjs/DateTimePicker';
import {
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    SearchInputField,
    Dropdown,
    SaveBackCancelButtons,
    DatePicker
} from '@linn-it/linn-form-components-library';
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
    worksOrders,
    worksOrdersLoading,
    boardParts,
    pcasRevisions,
    cits,
    smtShifts,
    employees,
    faultCodes,
    addItem,
    updateItem,
    itemId,
    history
}) {
    const [searchTerm, setSearchTerm] = useState(null);
    const [partFail, setPartFail] = useState({
        dateCreated: new Date().toISOString()
    });

    const [prevPartFail, setPrevPartFail] = useState({});

    const creating = () => editStatus === 'create';
    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';
    const aoiEscapeValues = ['', 'Y', 'N'];
    const inputInvalid = () => false;

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
                faultDescription: faultCodes.find(c => c.faultCode === a.faultCode)?.faultDescription
            }));
        }
    }, [faultCodes, partFail.faultCode]);


    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPartFail({ ...partFail, [propertyName]: newValue });
    };

    const handleSearchTermChange = (...args) => {
        setSearchTerm(args[1]);
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
        setSearchTerm('');
    };

    const handleBackClick = () => {
        history.push('/production/quality/part-fails');
    };

    return (
        <Page showRequestErrors>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Log An Part Fail" />
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
                            {!partFail?.worksOrderNumber ? (
                                <Fragment>
                                    <Grid item xs={4}>
                                        <SearchInputField
                                            label="Search for a Works Order to get started"
                                            fullWidth
                                            placeholder="Order Number"
                                            onChange={handleSearchTermChange}
                                            propertyName="searchTerm"
                                            type="number"
                                            value={searchTerm}
                                        />
                                    </Grid>
                                </Fragment>
                            ) : (
                                <Fragment />
                            )}
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
                            {partFail.worksOrderNumber || !creating() ? (
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
                                        <DateTimePicker
                                            value={partFail.dateTimeFound}
                                            label="Found"
                                            disabled
                                        />
                                    </Grid>
                                    {<Grid item xs={creating() ? 4 : 2} />}
                                    <Grid item xs={2}>
                                        <InputField
                                            disabled
                                            value={partFail.worksOrderNumber}
                                            type="number"
                                            label="Works Order"
                                            maxLength={8}
                                            onChange={handleFieldChange}
                                            propertyName="worksOrderNumber"
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={partFail.partNumber}
                                            label="Part Number"
                                            maxLength={10}
                                            onChange={handleFieldChange}
                                            propertyName="partNumber"
                                        />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            rows={3}
                                            value={partFail.partDescription}
                                            label="Description"
                                            maxLength={10}
                                            onChange={handleFieldChange}
                                            propertyName="partDescription"
                                        />
                                    </Grid>
                                    <Grid item xs={2} />
                                   
                                    <Grid item xs={4}>
                                        <InputField
                                            fullWidth
                                            rows={4}
                                            value={partFail.story}
                                            label="Story"
                                            onChange={handleFieldChange}
                                            propertyName="story"
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
                                    <Grid item xs={3}>
                                        <Dropdown
                                            label="Fault Code"
                                            propertyName="faultCode"
                                            items={[''].concat(faultCodes?.map(p => p.faultCode))}
                                            fullWidth
                                            value={partFail.faultCode}
                                            onChange={handleFieldChange}
                                        />
                                    </Grid>
                                    <Grid item xs={3}>
                                        <InputField
                                            fullWidth
                                            disabled
                                            value={partFail.faultDescription}
                                            label="Description"
                                            onChange={handleFieldChange}
                                            propertyName="faultDescription"
                                        />
                                    </Grid>
                                </Fragment>
                            ) : (
                                <Fragment />
                            )}
                            {worksOrdersLoading && (
                                <Grid item xs={12}>
                                    <Loading />
                                </Grid>
                            )}
                            {!worksOrdersLoading && worksOrders && worksOrders.length > 1 && (
                                <Typography>
                                    Refine your search.. more than one Works Order returned.
                                </Typography>
                            )}
                            {!worksOrdersLoading &&
                                searchTerm &&
                                worksOrders &&
                                worksOrders.length === 0 && (
                                    <Typography>
                                        No results to match the search criteria.
                                    </Typography>
                                )}
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
    boardParts: PropTypes.arrayOf(PropTypes.shape({})),
    item: PropTypes.shape({}),
    itemErrors: PropTypes.arrayOf(
        PropTypes.shape({
            status: PropTypes.number,
            statusText: PropTypes.string,
            details: PropTypes.shape({}),
            item: PropTypes.string
        })
    ),
    worksOrders: PropTypes.arrayOf(PropTypes.shape({})),
    pcasRevisions: PropTypes.arrayOf(PropTypes.shape({})),
    fetchPcasRevisionsForBoardPart: PropTypes.func,
    cits: PropTypes.arrayOf(PropTypes.shape({})),
    smtShifts: PropTypes.arrayOf(PropTypes.shape({})),
    employees: PropTypes.arrayOf(PropTypes.shape({})),
    faultCodes: PropTypes.arrayOf(PropTypes.shape({})),
    addItem: PropTypes.func,
    updateItem: PropTypes.func,
    itemId: PropTypes.number,
    fetchItems: PropTypes.func,
    worksOrdersLoading: PropTypes.bool
};

PartFail.defaultProps = {
    snackbarVisible: false,
    loading: null,
    profile: { employee: '', name: '' },
    boardParts: [],
    item: null,
    itemErrors: [],
    pcasRevisions: [],
    worksOrders: [],
    faultCodes: [],
    addItem: null,
    updateItem: null,
    itemId: null,
    cits: [],
    smtShifts: [],
    employees: [],
    fetchPcasRevisionsForBoardPart: null,
    fetchItems: null,
    worksOrdersLoading: true
};

export default PartFail;
