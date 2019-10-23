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
import TableWithInlineEditing from './TableWithInlineEditing';

function ManufacturingRoute({
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
    manufacturingSkills,
    manufacturingResources,
    cits,
    setSnackbarVisible
}) {
    const [manufacturingRoute, setManufacturingRoute] = useState({});
    const [prevManufacturingRoute, setPrevManufacturingRoute] = useState({});

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevManufacturingRoute) {
            setManufacturingRoute(item);
            setPrevManufacturingRoute(item);
        }
    }, [item, prevManufacturingRoute]);

    const RouteCodeInvalid = () => !manufacturingRoute.routeCode;
    const descriptionInvalid = () => !manufacturingRoute.description;
    const notesInvalid = () => !manufacturingRoute.notes;

    const inputInvalid = () => RouteCodeInvalid() || descriptionInvalid() || notesInvalid();

    const handleSaveClick = () => {
        if (editing()) {
            updateItem(itemId, manufacturingRoute);
            setEditStatus('view');
        } else if (creating()) {
            addItem(manufacturingRoute);
            setEditStatus('view');
        }
    };

    const handleCancelClick = () => {
        setManufacturingRoute(item);
        setEditStatus('view');
    };

    const handleBackClick = () => {
        history.push('/production/resources/manufacturing-routes/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setManufacturingRoute({ ...manufacturingRoute, [propertyName]: newValue });
    };

    const updateOp = ops => {
        handleFieldChange('operations', ops);
    };

    const OperationsTableAndInfo = () => {
        const skillCodes = manufacturingSkills.map(skill => skill.skillCode);
        const resourceCodes = manufacturingResources.map(resource => resource.resourceCode);
        const citCodes = cits.map(cit => cit.code);

        const columnsInfo = [
            {
                title: 'Operation Number',
                key: 'operationNumber',
                type: 'number'
            },
            {
                title: 'Description',
                key: 'description',
                type: 'text'
            },
            {
                title: 'CIT Code',
                key: 'cITCode',
                type: 'dropdown',
                options: citCodes
            },
            {
                title: 'Skill Code',
                key: 'skillCode',
                type: 'dropdown',
                options: skillCodes
            },
            {
                title: 'Set & Clean Time mins',
                key: 'setAndCleanTime',
                type: 'number'
            },
            {
                title: 'Resource Code',
                key: 'resourceCode',
                type: 'dropdown',
                options: resourceCodes
            },
            {
                title: 'Cycle Time mins',
                key: 'cycleTime',
                type: 'number'
            },
            {
                title: 'Labour Percentage',
                key: 'labourPercentage',
                type: 'number'
            }
        ];
        return (
            <TableWithInlineEditing
                columnsInfo={columnsInfo}
                content={manufacturingRoute.operations}
                updateContent={updateOp}
                editStatus={editStatus}
                allowedToEdit
            />
        );
    };

    return (
        <Page>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {creating() ? (
                        <Title text="Create Manufacturing Route" />
                    ) : (
                        <Title text="Manufacturing Route" />
                    )}
                </Grid>
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}
                {loading || !manufacturingRoute ? (
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
                                value={manufacturingRoute.routeCode}
                                label="Route Code"
                                maxLength={10}
                                helperText={
                                    !creating()
                                        ? 'This field cannot be changed'
                                        : `${RouteCodeInvalid() ? 'This field is required' : ''}`
                                }
                                required
                                onChange={handleFieldChange}
                                propertyName="RouteCode"
                            />
                        </Grid>
                        <Grid item xs={8}>
                            <InputField
                                value={manufacturingRoute.description}
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
                                value={manufacturingRoute.notes}
                                label="Notes"
                                type="multiline"
                                maxLength={300}
                                fullWidth
                                helperText={notesInvalid() ? 'This field is required' : ''}
                                required
                                onChange={handleFieldChange}
                                propertyName="notes"
                            />
                        </Grid>

                        {!creating() && manufacturingRoute.operations && OperationsTableAndInfo()}

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

ManufacturingRoute.propTypes = {
    item: PropTypes.shape({
        routeCode: PropTypes.string,
        description: PropTypes.string,
        notes: PropTypes.number
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
    setSnackbarVisible: PropTypes.func.isRequired,
    manufacturingSkills: PropTypes.shape({
        skillCode: PropTypes.string,
        description: PropTypes.string
    }).isRequired,
    manufacturingResources: PropTypes.shape({
        skillCode: PropTypes.string,
        description: PropTypes.string
    }).isRequired,
    cits: PropTypes.shape({
        code: PropTypes.string
    }).isRequired
};

ManufacturingRoute.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    errorMessage: '',
    itemId: null
};

export default ManufacturingRoute;
