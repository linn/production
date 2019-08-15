import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function AssemblyFail({
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
    const [assemblyFail, setAssemblyFail] = useState({});
    const [prevAssemblyFail, setPrevAssemblyFail] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevAssemblyFail) {
            setAssemblyFail(item);
            setPrevAssemblyFail(item);
        }
    }, [item, prevAssemblyFail]);

    const faultCodeInvalid = () => !assemblyFail.faultCode;
    const descriptionInvalid = () => !assemblyFail.description;

    const inputInvalid = () => faultCodeInvalid() || descriptionInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, assemblyFail);
            setEditStatus('view');
        } else if (creating()) {
            addItem(assemblyFail);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setAssemblyFail(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/quality/ate/fault-codes/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setAssemblyFail({ ...assemblyFail, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Assembly Fail" />
                    ) : (
                        <Title text="Assembly Fail Details" />
                    )}
                </Grid>
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}
                {loading || !assemblyFail ? (
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
                                value={assemblyFail.id}
                                label="Id"
                                maxLength={10}
                                helperText={
                                    !creating()
                                        ? 'This field cannot be changed'
                                        : `${faultCodeInvalid() ? 'This field is required' : ''}`
                                }
                                required
                                onChange={handleFieldChange}
                                propertyName="faultCode"
                            />
                        </Grid>
                        {/* <Grid item xs={8}>
                            <InputField
                                value={assemblyFail.description}
                                label="Description"
                                maxLength={50}
                                fullWidth
                                helperText={descriptionInvalid() ? 'This field is required' : ''}
                                required
                                onChange={handleFieldChange}
                                propertyName="description"
                            />
                        </Grid>
                        <Grid item xs={8}>
                            <InputField
                                value={
                                    assemblyFail.dateInvalid
                                        ? moment(assemblyFail.dateInvalid).format('YYYY-MM-DD')
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
                        </Grid> */}
                    </Fragment>
                )}
            </Grid>
        </Page>
    );
}

AssemblyFail.propTypes = {
    item: PropTypes.shape({
        assemblyFail: PropTypes.string,
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

AssemblyFail.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    errorMessage: '',
    itemId: null
};

export default AssemblyFail;
