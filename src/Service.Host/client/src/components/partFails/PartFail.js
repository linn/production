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

function PartFail({
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
    const [partFail, setPartFail] = useState({});
    const [prevPartFail, setPrevPartFail] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevPartFail) {
            setPartFail(item);
            setPrevPartFail(item);
        }
    }, [item, prevPartFail]);

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, partFail);
            setEditStatus('view');
        } else if (creating()) {
            addItem(partFail);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setPartFail(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/quality/part-fails');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setPartFail({ ...partFail, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Part Fail" />
                    ) : (
                        <Title text="Part Fail Details" />
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
                    partFail && (
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
                                    value={partFail.Id}
                                    label="Id"
                                    maxLength={10}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="id"
                                />
                            </Grid>
                            {/* <Grid item xs={8}>
                                <InputField
                                    value={partFail.description}
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
                                    value={partFail.hourlyRate}
                                    label="Hourly Rate"
                                    type="number"
                                    maxLength={3}
                                    fullWidth
                                    helperText={hourlyRateInvalid() ? 'This field is required' : ''}
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="hourlyRate"
                                />
                            </Grid> */}
                            <Grid item xs={12}>
                                <SaveBackCancelButtons
                                    saveDisabled={viewing()}
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

PartFail.propTypes = {
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

PartFail.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null
};

export default PartFail;
