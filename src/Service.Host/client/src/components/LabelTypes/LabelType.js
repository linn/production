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

function LabelType({
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
    const [labelType, setLabelType] = useState({});
    const [prevLabelType, setPrevLabelType] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevLabelType) {
            setLabelType(item);
            setPrevLabelType(item);
        }
    }, [item, prevLabelType]);

    const skillCodeInvalid = () => !labelType.skillCode;
    const descriptionInvalid = () => !labelType.description;
    const hourlyRateInvalid = () => !labelType.hourlyRate;

    const inputInvalid = () => skillCodeInvalid() || descriptionInvalid() || hourlyRateInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, labelType);
            setEditStatus('view');
        } else if (creating()) {
            addItem(labelType);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setLabelType(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/resources/label-types/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setLabelType({ ...labelType, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? <Title text="Create Label Type" /> : <Title text="Label Type" />}
                </Grid>
                {itemError && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemError.statusText} />
                    </Grid>
                )}
                {loading ? (
                    <Grid item xs={12}>
                        <Loading />
                    </Grid>
                ) : (
                    LabelType && (
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
                                    value={labelType.skillCode}
                                    label="Skill Code"
                                    maxLength={10}
                                    helperText={
                                        !creating()
                                            ? 'This field cannot be changed'
                                            : `${
                                                  skillCodeInvalid() ? 'This field is required' : ''
                                              }`
                                    }
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="skillCode"
                                />
                            </Grid>
                            <Grid item xs={8}>
                                <InputField
                                    value={labelType.description}
                                    label="Description"
                                    maxLength={50}
                                    fullWidth
                                    helperText={
                                        descriptionInvalid() ? 'This field is required' : ''
                                    }
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="description"
                                />
                            </Grid>
                            <Grid item xs={8}>
                                <InputField
                                    value={labelType.hourlyRate}
                                    label="Hourly Rate"
                                    type="number"
                                    maxLength={3}
                                    fullWidth
                                    helperText={hourlyRateInvalid() ? 'This field is required' : ''}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="hourlyRate"
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

LabelType.propTypes = {
    item: PropTypes.shape({
        skillCode: PropTypes.string,
        description: PropTypes.string,
        hourlyRate: PropTypes.number
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({}),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

LabelType.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null
};

export default LabelType;
