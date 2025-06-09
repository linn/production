import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function ManufacturingSkill({
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
    const [manufacturingSkill, setManufacturingSkill] = useState({});
    const [prevManufacturingSkill, setPrevManufacturingSkill] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevManufacturingSkill) {
            setManufacturingSkill(item);
            setPrevManufacturingSkill(item);
        }
    }, [item, prevManufacturingSkill]);

    const skillCodeInvalid = () => !manufacturingSkill.skillCode;
    const descriptionInvalid = () => !manufacturingSkill.description;
    const hourlyRateInvalid = () => !manufacturingSkill.hourlyRate;

    const inputInvalid = () => skillCodeInvalid() || descriptionInvalid() || hourlyRateInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, manufacturingSkill);
            setEditStatus('view');
        } else if (creating()) {
            addItem(manufacturingSkill);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setManufacturingSkill(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/resources');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setManufacturingSkill({ ...manufacturingSkill, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Manufacturing Skill" />
                    ) : (
                        <Title text="Manufacturing Skill" />
                    )}
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
                    manufacturingSkill && (
                        <>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message="Save Successful"
                            />
                            <Grid item xs={8}>
                                <InputField
                                    fullWidth
                                    disabled={!creating()}
                                    value={manufacturingSkill.skillCode}
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
                                    value={manufacturingSkill.description}
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
                                    value={manufacturingSkill.hourlyRate}
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
                            {!creating() && (
                                <>
                                    <Grid item xs={8}>
                                        <InputField
                                            value={
                                                manufacturingSkill.dateInvalid
                                                    ? moment(manufacturingSkill.dateInvalid).format(
                                                          'DD-MMM-YYYY'
                                                      )
                                                    : ''
                                            }
                                            disabled
                                            fullWidth
                                            label="Date Invalid"
                                            propertyName="dateInvalid"
                                        />
                                    </Grid>
                                    <Grid item xs={8}>
                                        {!manufacturingSkill.dateInvalid && (
                                            <Button
                                                onClick={() =>
                                                    handleFieldChange('dateInvalid', new Date())
                                                }
                                            >
                                                Make Invalid
                                            </Button>
                                        )}
                                    </Grid>
                                </>
                            )}
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing() || inputInvalid()}
                                    saveClick={handleSaveClick}
                                    cancelClick={handleCancelClick}
                                    backClick={handleBackClick}
                                />
                            </Grid>
                        </>
                    )
                )}
            </Grid>
        </Page>
    );
}

ManufacturingSkill.propTypes = {
    item: PropTypes.shape({
        skillCode: PropTypes.string,
        description: PropTypes.string,
        hourlyRate: PropTypes.number
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemError: PropTypes.shape({ statusText: PropTypes.string }),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired
};

ManufacturingSkill.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null
};

export default ManufacturingSkill;
