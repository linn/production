import React, { Fragment, useState, useEffect, useCallback } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    TableWithInlineEditing,
    utilities
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function ManufacturingRoute({
    editStatus,
    itemErrors,
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
    const [allowedToEdit, setAllowedToEdit] = useState(false);

    const creating = useCallback(() => editStatus === 'create', [editStatus]);
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    useEffect(() => {
        if (item !== prevManufacturingRoute) {
            setManufacturingRoute(item);
            setPrevManufacturingRoute(item);
        }

        setAllowedToEdit(utilities.getHref(item, 'edit') !== null);
    }, [item, prevManufacturingRoute, editStatus, creating]);

    const RouteCodeInvalid = () => !manufacturingRoute.routeCode;
    const operationsComplete = () =>
        creating() ||
        manufacturingRoute.operations.every(
            x =>
                x.operationNumber &&
                x.description &&
                x.cITCode &&
                x.skillCode &&
                x.setAndCleanTime >= 0 &&
                x.resourceCode &&
                x.cycleTime >= 0 &&
                x.labourPercentage >= 0 &&
                x.labourPercentage <= 100
        );

    const inputInvalid = () => RouteCodeInvalid() || !operationsComplete();

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
        setEditStatus('view');
        setManufacturingRoute(item);
    };

    const handleBackClick = () => {
        history.push('/production/resources/manufacturing-routes/');
    };

    const handleResourceFieldChange = (propertyName, newValue) => {
        setManufacturingRoute({ ...manufacturingRoute, [propertyName]: newValue });
        if (viewing()) {
            setEditStatus('edit');
        }
    };

    const updateOp = ops => {
        handleResourceFieldChange('operations', ops);
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
                content={manufacturingRoute.operations.map(o => ({ ...o, id: o.manufacturingId }))}
                updateContent={updateOp}
                editStatus={editStatus}
                allowedToEdit={allowedToEdit}
                allowedToCreate={allowedToEdit}
                allowedToDelete={allowedToEdit}
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
                {itemErrors && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={itemErrors.statusText} />
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
                                onChange={handleResourceFieldChange}
                                propertyName="routeCode"
                            />
                        </Grid>
                        <Grid item xs={8}>
                            <InputField
                                value={manufacturingRoute.description}
                                label="Description"
                                maxLength={50}
                                fullWidth
                                onChange={handleResourceFieldChange}
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
                                onChange={handleResourceFieldChange}
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
        notes: PropTypes.string
    }),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    editStatus: PropTypes.string.isRequired,
    itemErrors: PropTypes.shape({}),
    itemId: PropTypes.string,
    snackbarVisible: PropTypes.bool,
    updateItem: PropTypes.func,
    addItem: PropTypes.func,
    loading: PropTypes.bool,
    setEditStatus: PropTypes.func.isRequired,
    setSnackbarVisible: PropTypes.func.isRequired,
    manufacturingSkills: PropTypes.arrayOf(
        PropTypes.shape({
            skillCode: PropTypes.string,
            description: PropTypes.string
        })
    ),
    manufacturingResources: PropTypes.arrayOf(
        PropTypes.shape({
            skillCode: PropTypes.string,
            description: PropTypes.string
        })
    ),
    cits: PropTypes.arrayOf(
        PropTypes.shape({
            code: PropTypes.string
        })
    )
};

ManufacturingRoute.defaultProps = {
    item: {},
    snackbarVisible: false,
    addItem: null,
    updateItem: null,
    loading: null,
    itemErrors: null,
    itemId: null,
    cits: [],
    manufacturingResources: [],
    manufacturingSkills: []
};

export default ManufacturingRoute;
