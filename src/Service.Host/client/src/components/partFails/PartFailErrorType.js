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
    DatePicker
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function PartFailErrorType({
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
    setSnackbarVisible
}) {
    const [partFailErrorType, setpartFailErrorType] = useState({});
    const [prevpartFailErrorType, setPrevpartFailErrorType] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevpartFailErrorType) {
            setpartFailErrorType(item);
            setPrevpartFailErrorType(item);
        }
    }, [item, prevpartFailErrorType]);

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, partFailErrorType);
            setEditStatus('view');
        } else if (creating()) {
            addItem(partFailErrorType);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setpartFailErrorType(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/quality/part-fail-error-types/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setpartFailErrorType({ ...partFailErrorType, [propertyName]: newValue });
    };
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
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Part Fail Error Type" />
                    ) : (
                        <Title text="Part Fail Error Type" />
                    )}
                </Grid>
                {itemError ? (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                ) : (
                    partFailErrorType && (
                        <Fragment>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            <Grid item xs={8}>
                                <InputField
                                    fullWidth
                                    disabled={!creating()}
                                    value={partFailErrorType.errorType}
                                    label="Error Type"
                                    maxLength={10}
                                    helperText={
                                        !creating()
                                            ? 'This field cannot be changed'
                                            : 'This field is required'
                                    }
                                    required={creating()}
                                    onChange={handleFieldChange}
                                    propertyName="errorType"
                                />
                            </Grid>
                            <Grid item xs={4}>
                                <DatePicker
                                    label="Date Invalid"
                                    value={
                                        partFailErrorType.dateInvalid
                                            ? partFailErrorType.dateInvalid
                                            : null
                                    }
                                    onChange={value => {
                                        handleFieldChange('dateInvalid', value);
                                    }}
                                />
                            </Grid>
                        </Fragment>
                    )
                )}
                <Grid item xs={12}>
                    <SaveBackCancelButtons
                        saveDisabled={viewing() || !partFailErrorType.errorType}
                        saveClick={handleSaveClick}
                        cancelClick={handleCancelClick}
                        backClick={handleBackClick}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

PartFailErrorType.propTypes = {
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
    setSnackbarVisible: PropTypes.func.isRequired
};

PartFailErrorType.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemId: null,
    itemError: null
};

export default PartFailErrorType;
