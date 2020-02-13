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
import Page from '../../containers/Page';

function WorksOrderLabel({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    updateItem,
    addItem,
    setEditStatus,
    setSnackbarVisible,
    partsSearchResults,
    searchParts,
    partsSearchLoading,
    clearPartsSearch
}) {
    const [worksOrderLabel, setWorksOrderLabel] = useState({});
    const [prevWorksOrderLabel, setPrevworksOrderLabel] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevWorksOrderLabel) {
            setWorksOrderLabel(item);
            setPrevworksOrderLabel(item);
        }
    }, [item, prevWorksOrderLabel]);

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, worksOrderLabel);
            setEditStatus('view');
        } else if (creating()) {
            addItem(worksOrderLabel);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setWorksOrderLabel(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/works-orders/labels/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setWorksOrderLabel({ ...worksOrderLabel, [propertyName]: newValue });
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
                    worksOrderLabel && (
                        <>
                            <Grid item xs={12}>
                                <Title text="Works Order Label" />
                            </Grid>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            {creating() ? (
                                <>
                                    <Grid item xs={5}>
                                        <InputField
                                            label="Part (click search icon to change)"
                                            maxLength={14}
                                            fullWidth
                                            value={worksOrderLabel.partNumber}
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
                                                    setWorksOrderLabel(a => ({
                                                        ...a,
                                                        partNumber: newValue.partNumber
                                                    }));
                                                }}
                                                searchItems={partsSearchResults}
                                                loading={partsSearchLoading}
                                                fetchItems={searchParts}
                                                clearSearch={() => clearPartsSearch}
                                            />
                                        </div>
                                    </Grid>{' '}
                                </>
                            ) : (
                                <Grid item xs={12}>
                                    <InputField
                                        fullWidth
                                        value={worksOrderLabel.sequence}
                                        label="Sequence"
                                        disabled
                                        maxLength={10}
                                        onChange={handleFieldChange}
                                        propertyName="sequence"
                                    />
                                </Grid>
                            )}
                            <Grid item xs={12}>
                                <InputField
                                    fullWidth
                                    value={worksOrderLabel.labelText}
                                    label="Label Text"
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="labelText"
                                />
                            </Grid>
                        </>
                    )
                )}
                <Grid item xs={12}>
                    <SaveBackCancelButtons
                        saveDisabled={
                            viewing() || !worksOrderLabel.labelText || !worksOrderLabel.partNumber
                        }
                        saveClick={handleSaveClick}
                        cancelClick={handleCancelClick}
                        backClick={handleBackClick}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

WorksOrderLabel.propTypes = {
    item: PropTypes.shape({}),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    itemError: PropTypes.shape({}),
    setSnackbarVisible: PropTypes.func.isRequired,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    searchParts: PropTypes.func,
    partsSearchLoading: PropTypes.bool,
    clearPartsSearch: PropTypes.func
};

WorksOrderLabel.defaultProps = {
    item: {},
    snackbarVisible: false,
    loading: null,
    itemId: null,
    itemError: null,
    updateItem: null,
    addItem: null,
    partsSearchResults: [],
    searchParts: null,
    partsSearchLoading: false,
    clearPartsSearch: null
};

export default WorksOrderLabel;
