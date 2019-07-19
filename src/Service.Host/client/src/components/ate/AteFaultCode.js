import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import { Grid } from '@material-ui/core';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function AteFaultCode({
    editStatus,
    errorMessage,
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
    const [ateFaultCode, setAteFaultCode] = useState({});
    const [prevAteFaultCode, setPrevAteFaultCode] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevAteFaultCode) {
            setAteFaultCode(item);
            setPrevAteFaultCode(item);
        }
    }, [item, prevAteFaultCode]);

    const faultCodeInvalid = () => !ateFaultCode.faultCode;
    const descriptionInvalid = () => !ateFaultCode.description;

    const inputInvalid = () => faultCodeInvalid() || descriptionInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, ateFaultCode);
            setEditStatus('view');
        } else if (creating()) {
            addItem(ateFaultCode);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setAteFaultCode(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/quality/ate/fault-codes/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setAteFaultCode({ ...ateFaultCode, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create ATE Fault Code" />
                    ) : (
                        <Title text="ATE Fault Code" />
                    )}
                </Grid>
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}
                {loading || !ateFaultCode ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
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
                                value={ateFaultCode.faultCode}
                                label="Sequence Name"
                                maxLength={10}
                                helperText={
                                    !creating()
                                        ? 'This field cannot be changed'
                                        : `${faultCodeInvalid() ? 'This field is required' : ''}`
                                }
                                error={faultCodeInvalid()}
                                onChange={handleFieldChange}
                                propertyName="faultCode"
                            />
                        </Grid>
                        <Grid item xs={8}>
                            <InputField
                                value={ateFaultCode.description}
                                label="Description"
                                maxLength={50}
                                fullWidth
                                helperText={descriptionInvalid() ? 'This field is required' : ''}
                                error={descriptionInvalid()}
                                onChange={handleFieldChange}
                                propertyName="description"
                            />
                        </Grid>
                        <Grid item xs={8}>
                            <InputField
                                value={
                                    ateFaultCode.dateInvalid
                                        ? moment(ateFaultCode.dateInvalid).format('YYYY-MM-DD')
                                        : ''
                                }
                                label="Date Invalid"
                                fullWidth
                                onChange={handleFieldChange}
                                propertyName="dateInvalid"
                                type="date"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <SaveBackCancelButtons
                                saveDisabled={viewing() || inputInvalid()}
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

AteFaultCode.propTypes = {
    item: PropTypes.shape({
        ateFaultCode: PropTypes.string,
        description: PropTypes.string,
        nextSerialNumber: PropTypes.number,
        dateClosed: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    errorMessage: PropTypes.string,
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

AteFaultCode.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    errorMessage: '',
    itemId: null
};

export default AteFaultCode;
