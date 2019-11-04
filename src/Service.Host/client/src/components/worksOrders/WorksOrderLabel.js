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

function WorksOrderLabel({
    editStatus,
    itemError,
    history,
    itemId,
    item,
    loading,
    snackbarVisible,
    updateItem,
    setEditStatus,
    setSnackbarVisible
}) {
    const [worksOrderLabel, setworksOrderLabel] = useState({});
    const [prevworksOrderLabel, setPrevworksOrderLabel] = useState({});

    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevworksOrderLabel) {
            setworksOrderLabel(item);
            setPrevworksOrderLabel(item);
        }
    }, [item, prevworksOrderLabel]);

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, worksOrderLabel);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setworksOrderLabel(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/works-orders/labels/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setworksOrderLabel({ ...worksOrderLabel, [propertyName]: newValue });
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
                {itemError ? (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                ) : (
                    worksOrderLabel && (
                        <Fragment>
                            <Grid item xs={12}>
                                <Title text={`${worksOrderLabel.partNumber} Works Order Label`} />
                            </Grid>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
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
                            <Grid item xs={12}>
                                <InputField
                                    fullWidth
                                    value={worksOrderLabel.labelText}
                                    label="Label Text"
                                    onChange={handleFieldChange}
                                    propertyName="labelText"
                                />
                            </Grid>
                        </Fragment>
                    )
                )}
                <Grid item xs={12}>
                    <SaveBackCancelButtons
                        saveDisabled={
                            viewing() || !worksOrderLabel.sequence || !worksOrderLabel.labelText
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
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    itemError: PropTypes.shape({}),
    setSnackbarVisible: PropTypes.func.isRequired
};

WorksOrderLabel.defaultProps = {
    item: {},
    snackbarVisible: false,
    updateItem: null,
    loading: null,
    itemId: null,
    itemError: null
};

export default WorksOrderLabel;
