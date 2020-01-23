import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    Dropdown,
    TypeaheadDialog,
    utilities
} from '@linn-it/linn-form-components-library';
import { makeStyles } from '@material-ui/styles';
import Page from '../../containers/Page';

function LabelReprint({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    addItem,
    updateItem,
    setEditStatus,
    setSnackbarVisible,
    labelTypes,
    partsSearchResults,
    partsSearchLoading,
    searchParts,
    clearPartsSearch,
    clearErrors,
    applicationState
}) {
    const [labelReprint, setLabelReprint] = useState({
        numberOfProducts: 1,
        labelTypeCode: 'BOX',
        reprintType: 'REPRINT'
    });
    const [prevLabelReprint, setPrevLabelReprint] = useState(null);

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item && item !== prevLabelReprint) {
            setLabelReprint(item);
            setPrevLabelReprint(item);
        }
    }, [item, prevLabelReprint]);

    const reasonInvalid = () => !labelReprint.reason;
    const serialNumberInvalid = () => !labelReprint.serialNumber;
    const inputInvalid = () => reasonInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, labelReprint);
            setEditStatus('view');
        } else if (creating()) {
            clearErrors();
            addItem(labelReprint);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setLabelReprint(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/maintenance');
    };

    const setPart = field => newValue => {
        setLabelReprint({ ...labelReprint, [field]: newValue.id });
        clearPartsSearch();
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }

        setLabelReprint({ ...labelReprint, [propertyName]: newValue });
    };

    const useStyles = makeStyles(theme => ({
        marginTop: {
            marginTop: theme.spacing(2)
        }
    }));
    const classes = useStyles();

    const optionsAllowed = () => {
        if (utilities.getHref(applicationState, 'create')) {
            return ['REPRINT', 'REISSUE', 'RSN REPRINT', 'REBUILD'];
        }

        return ['REPRINT'];
    };

    return (
        <Fragment>
            <Grid container alignItems="center" justify="center">
                <Grid xs={6} item>
                    <Page>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                <Fragment>
                                    <Button
                                        href="/production/maintenance/labels/print"
                                        variant="outlined"
                                        color="primary"
                                        style={{ float: 'right' }}
                                    >
                                        print new
                                    </Button>
                                </Fragment>
                                <Title text="Reprint / Reissue / Rebuild" />
                            </Grid>
                            {itemError && (
                                <Grid item xs={12}>
                                    <ErrorCard errorMessage={itemError} />
                                </Grid>
                            )}
                            {loading ? (
                                <Grid item xs={12}>
                                    <Loading />
                                </Grid>
                            ) : (
                                labelReprint && (
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
                                                value={labelReprint.labelReprintId}
                                                label="Id"
                                                required
                                                propertyName="labelReprintId"
                                            />
                                        </Grid>
                                        <Grid item xs={8} />
                                        <Grid item xs={4}>
                                            <Dropdown
                                                fullWidth
                                                label="Reprint Type"
                                                propertyName="reprintType"
                                                disabled={!creating()}
                                                allowNoValue={false}
                                                items={optionsAllowed()}
                                                value={labelReprint.reprintType}
                                                onChange={handleFieldChange}
                                            />
                                        </Grid>
                                        <Grid item xs={8} />
                                        <Grid item xs={12}>
                                            <InputField
                                                value={labelReprint.reason}
                                                disabled={!creating()}
                                                label="Reason"
                                                maxLength={50}
                                                fullWidth
                                                helperText={
                                                    reasonInvalid() ? 'This field is required' : ''
                                                }
                                                required
                                                onChange={handleFieldChange}
                                                propertyName="reason"
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <InputField
                                                label="Part Number"
                                                maxLength={14}
                                                fullWidth
                                                value={labelReprint.partNumber}
                                                onChange={handleFieldChange}
                                                propertyName="partNumber"
                                            />
                                        </Grid>
                                        <Grid item xs={2} className={classes.marginTop}>
                                            <TypeaheadDialog
                                                title="Search For Part"
                                                onSelect={setPart('partNumber')}
                                                searchItems={partsSearchResults}
                                                loading={partsSearchLoading}
                                                fetchItems={searchParts}
                                                clearSearch={() => clearPartsSearch}
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <InputField
                                                label="New Part Number"
                                                maxLength={14}
                                                fullWidth
                                                value={labelReprint.newPartNumber}
                                                onChange={handleFieldChange}
                                                propertyName="newPartNumber"
                                            />
                                        </Grid>
                                        <Grid item xs={2} className={classes.marginTop}>
                                            <TypeaheadDialog
                                                title="Search For Part"
                                                onSelect={setPart('newPartNumber')}
                                                searchItems={partsSearchResults}
                                                loading={partsSearchLoading}
                                                fetchItems={searchParts}
                                                clearSearch={() => clearPartsSearch}
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <InputField
                                                value={labelReprint.serialNumber}
                                                disabled={!creating()}
                                                label="Serial Number"
                                                maxLength={50}
                                                fullWidth
                                                type="number"
                                                helperText={
                                                    serialNumberInvalid()
                                                        ? 'This field is required'
                                                        : ''
                                                }
                                                required
                                                onChange={handleFieldChange}
                                                propertyName="serialNumber"
                                            />
                                        </Grid>
                                        <Grid item xs={12}>
                                            <Dropdown
                                                label="Label Type"
                                                propertyName="labelTypeCode"
                                                disabled={!creating()}
                                                items={labelTypes.map(s => ({
                                                    id: s.labelTypeCode,
                                                    displayText: `${s.labelTypeCode}`
                                                }))}
                                                value={labelReprint.labelTypeCode || ''}
                                                onChange={handleFieldChange}
                                                allowNoValue={false}
                                            />
                                        </Grid>
                                        <Grid item xs={4}>
                                            <InputField
                                                value={labelReprint.numberOfProducts}
                                                disabled={!creating()}
                                                label="No Of Products"
                                                fullWidth
                                                required
                                                onChange={handleFieldChange}
                                                propertyName="numberOfProducts"
                                            />
                                        </Grid>
                                        <Grid item xs={10} />
                                        <Grid item xs={12}>
                                            <SaveBackCancelButtons
                                                saveDisabled={viewing() || inputInvalid()}
                                                saveClick={handleSaveClick}
                                                cancelClick={handleCancelClick}
                                                backClick={handleBackClick}
                                            />
                                        </Grid>
                                    </Fragment>
                                )
                            )}
                        </Grid>
                    </Page>{' '}
                </Grid>
            </Grid>
        </Fragment>
    );
}

LabelReprint.propTypes = {
    item: PropTypes.shape({
        labelReprint: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.oneOfType([
        PropTypes.string,
        PropTypes.shape({
            status: PropTypes.number,
            statusText: PropTypes.string,
            details: PropTypes.shape({}),
            item: PropTypes.string
        })
    ]),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    labelTypes: PropTypes.arrayOf(PropTypes.shape({})),
    partsSearchLoading: PropTypes.bool,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired,
    clearErrors: PropTypes.func.isRequired,
    applicationState: PropTypes.shape({ links: PropTypes.arrayOf(PropTypes.shape({})) })
};

LabelReprint.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null,
    labelTypes: [],
    partsSearchResults: [],
    partsSearchLoading: false,
    applicationState: null
};

export default LabelReprint;
