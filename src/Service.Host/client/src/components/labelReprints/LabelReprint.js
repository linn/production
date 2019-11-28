import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
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
    setSnackbarVisible
}) {
    const [labelReprint, setLabelReprint] = useState({});
    const [prevLabelReprint, setPrevLabelReprint] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevLabelReprint) {
            setLabelReprint(item);
            setPrevLabelReprint(item);
        }
    }, [item, prevLabelReprint]);

    const reasonInvalid = () => !labelReprint.reason;
    const inputInvalid = () => reasonInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, labelReprint);
            setEditStatus('view');
        } else if (creating()) {
            addItem(labelReprint);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setLabelReprint(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/maintenance/labels/reprint-reasons');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }

        setLabelReprint({ ...labelReprint, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
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
                            <Grid item xs={8}>
                                <InputField
                                    fullWidth
                                    disabled
                                    value={labelReprint.labelReprintId}
                                    label="Id"
                                    required
                                    propertyName="labelReprintId"
                                />
                            </Grid>
                            <Grid item xs={8}>
                                <InputField
                                    value={labelReprint.reason}
                                    disabled={!creating()}
                                    label="Reason"
                                    maxLength={50}
                                    fullWidth
                                    helperText={reasonInvalid() ? 'This field is required' : ''}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="reason"
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
                    )
                )}
            </Grid>
        </Page>
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
    itemError: PropTypes.shape({
        status: PropTypes.number,
        statusText: PropTypes.string,
        details: PropTypes.shape({}),
        item: PropTypes.string
    }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

LabelReprint.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null
};

export default LabelReprint;
