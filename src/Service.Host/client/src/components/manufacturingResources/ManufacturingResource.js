import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    InputField
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function ManufacturingResource({
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
    const [manufacturingResource, setManufacturingResource] = useState({});
    const [prevManufacturingResource, setPrevManufacturingResource] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevManufacturingResource) {
            setManufacturingResource(item);
            setPrevManufacturingResource(item);
        }
    }, [item, prevManufacturingResource]);

    const resourceCodeInvalid = () => !manufacturingResource.resourceCode;

    const inputInvalid = () => resourceCodeInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, manufacturingResource);
            setEditStatus('view');
        } else if (creating()) {
            addItem(manufacturingResource);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setManufacturingResource(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/resources/manufacturing-resources/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setManufacturingResource({ ...manufacturingResource, [propertyName]: newValue });
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Manufacturing Resource" />
                    ) : (
                        <Title text="Manufacturing Resource" />
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
                    manufacturingResource && (
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
                                    value={manufacturingResource.resourceCode}
                                    label="Resource Code"
                                    maxLength={10}
                                    helperText={
                                        !creating()
                                            ? 'This field cannot be changed'
                                            : `${
                                                  resourceCodeInvalid()
                                                      ? 'This field is required'
                                                      : ''
                                              }`
                                    }
                                    required
                                    onChange={handleFieldChange}
                                    propertyName="resourceCode"
                                />
                            </Grid>
                            <Grid item xs={8}>
                                <InputField
                                    value={manufacturingResource.description}
                                    label="Description"
                                    maxLength={50}
                                    fullWidth
                                    onChange={handleFieldChange}
                                    propertyName="description"
                                />
                            </Grid>
                            <Grid item xs={8}>
                                <InputField
                                    value={manufacturingResource.cost}
                                    label="Cost"
                                    type="number"
                                    decimalPlaces={2}
                                    maxLength={14}
                                    fullWidth
                                    onChange={handleFieldChange}
                                    propertyName="cost"
                                />
                            </Grid>
                        </Fragment>
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

ManufacturingResource.propTypes = {
    item: PropTypes.shape({
        resourceCode: PropTypes.string,
        description: PropTypes.string,
        cost: PropTypes.number
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

ManufacturingResource.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemError: null,
    itemId: null
};

export default ManufacturingResource;
