import React, { useState, useEffect } from 'react';
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

function PartFailFaultCode({
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
    const [partFailFaultCode, setpartFailFaultCode] = useState({});
    const [prevpartFailFaultCode, setPrevpartFailFaultCode] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevpartFailFaultCode) {
            setpartFailFaultCode(item);
            setPrevpartFailFaultCode(item);
        }
    }, [item, prevpartFailFaultCode]);

    const descriptionInvalid = () => !partFailFaultCode.faultDescription;

    const inputInvalid = () => descriptionInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, partFailFaultCode);
            setEditStatus('view');
        } else if (creating()) {
            addItem(partFailFaultCode);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setpartFailFaultCode(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/quality/part-fail-fault-codes/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setpartFailFaultCode({ ...partFailFaultCode, [propertyName]: newValue });
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
                        <Title text="Create Part Fail Fault Code" />
                    ) : (
                        <Title text="Part Fail Fault Code" />
                    )}
                </Grid>
                {itemError ? (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError?.statusText} />
                    </Grid>
                ) : (
                    partFailFaultCode && (
                        <>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            <Grid item xs={6}>
                                <InputField
                                    fullWidth
                                    disabled={!creating()}
                                    value={partFailFaultCode.faultCode}
                                    label="Code"
                                    maxLength={10}
                                    helperText={
                                        !creating()
                                            ? 'This field cannot be changed'
                                            : 'This field is required'
                                    }
                                    required={creating()}
                                    onChange={handleFieldChange}
                                    propertyName="faultCode"
                                />
                            </Grid>
                            <Grid item xs={8}>
                                <InputField
                                    value={partFailFaultCode.faultDescription}
                                    label="Description"
                                    maxLength={50}
                                    fullWidth
                                    helperText={
                                        descriptionInvalid() ? 'This field is required' : ''
                                    }
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="faultDescription"
                                />
                            </Grid>
                            <Grid item xs={4}>
                                <DatePicker
                                    label="Date Invalid"
                                    value={
                                        partFailFaultCode.dateInvalid
                                            ? partFailFaultCode.dateInvalid
                                            : null
                                    }
                                    onChange={value => {
                                        handleFieldChange('dateInvalid', value);
                                    }}
                                />
                            </Grid>
                        </>
                    )
                )}
                <Grid item xs={12}>
                    <SaveBackCancelButtons
                        saveDisabled={viewing() || inputInvalid()}
                        saveClick={handleSaveClick}
                        cancelClick={handleCancelClick}
                        backClick={handleBackClick}
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

PartFailFaultCode.propTypes = {
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

PartFailFaultCode.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemId: null,
    itemError: null
};

export default PartFailFaultCode;
