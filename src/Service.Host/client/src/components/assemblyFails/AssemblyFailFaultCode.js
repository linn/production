import React, { useEffect, useState, Fragment } from 'react';
import PropTypes from 'prop-types';
import {
    Title,
    Loading,
    SnackbarMessage,
    InputField,
    SaveBackCancelButtons,
    DatePicker,
    ErrorCard
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import Page from '../../containers/Page';

export default function AssemblyFailFaultCode({
    editStatus,
    item,
    setEditStatus,
    addItem,
    updateItem,
    history,
    snackbarVisible,
    setSnackbarVisible,
    loading,
    error
}) {
    const [faultCode, setFaultCode] = useState({});
    const [prevFaultCode, setPrevFaultCode] = useState({});

    const viewing = () => editStatus === 'view';
    const editing = () => editStatus === 'edit';
    const creating = () => editStatus === 'create';

    useEffect(() => {
        if (item && item !== prevFaultCode) {
            setFaultCode(item);
            setPrevFaultCode(item);
        }
    }, [item, prevFaultCode]);

    const handleCancelClick = () => {
        setFaultCode(item);
        setEditStatus('view');
    };

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(faultCode.faultCode, faultCode);
            setEditStatus('view');
        } else if (creating()) {
            addItem(faultCode);
            setEditStatus('view');
        }
    };

    const handleBackClick = () => {
        setEditStatus('view');
        history.push('/production/quality/assembly-fail-fault-codes');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setFaultCode({ ...faultCode, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {viewing ? (
                        <Title text="Assembly Fail Fault Code" />
                    ) : (
                        <Title text="Create Assembly Fail Fault Code" />
                    )}
                </Grid>
                {error && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={error} />
                    </Grid>
                )}
                {loading ? (
                    <Loading />
                ) : (
                    <Fragment>
                        <SnackbarMessage
                            visible={snackbarVisible}
                            onClose={() => setSnackbarVisible(false)}
                            message="Save Successful"
                        />
                        <Grid item xs={4}>
                            <InputField
                                disabled={viewing() || editing()}
                                label="Fault Code"
                                fullWidth
                                maxLength={10}
                                required={!faultCode.faultCode}
                                helperText={!faultCode.faultCode ? 'This field is required' : ''}
                                onChange={handleFieldChange}
                                propertyName="faultCode"
                                value={faultCode?.faultCode}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                label="Description"
                                fullWidth
                                maxLength={50}
                                onChange={handleFieldChange}
                                propertyName="description"
                                value={faultCode?.description}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <InputField
                                label="Explanation"
                                fullWidth
                                maxLength={200}
                                onChange={handleFieldChange}
                                propertyName="explanation"
                                value={faultCode?.explanation}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={4}>
                            <DatePicker
                                label="Date Invalid"
                                value={faultCode.dateInvalid ? faultCode.dateInvalid : null}
                                onChange={value => {
                                    handleFieldChange('dateInvalid', value);
                                }}
                            />
                        </Grid>
                        <Grid item xs={8} />
                        <Grid item xs={12}>
                            <SaveBackCancelButtons
                                saveDisabled={viewing() || editing() || !faultCode?.faultCode}
                                saveClick={handleSaveClick}
                                cancelClick={handleCancelClick}
                                backClick={handleBackClick}
                            />
                        </Grid>
                    </Fragment>
                )}
            </Grid>
        </Page>
    );
}

AssemblyFailFaultCode.propTypes = {
    item: PropTypes.shape({
        faultCode: PropTypes.string,
        description: PropTypes.string,
        explanation: PropTypes.string,
        dateCancelled: PropTypes.string
    }),
    editStatus: PropTypes.bool.isRequired,
    setEditStatus: PropTypes.func.isRequired,
    addItem: PropTypes.func.isRequired,
    updateItem: PropTypes.func.isRequired,
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    snackbarVisible: PropTypes.bool,
    setSnackbarVisible: PropTypes.func.isRequired,
    loading: PropTypes.bool,
    error: PropTypes.string
};

AssemblyFailFaultCode.defaultProps = {
    item: {},
    loading: false,
    snackbarVisible: false,
    error: ''
};
