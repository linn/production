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
import { Table, TableHead, TableRow, TableCell, TableBody } from '@material-ui/core';
import Page from '../../containers/Page';
import Operation from './Operation';

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

    const RouteCodeInvalid = () => !manufacturingRoute.RouteCode;
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
        history.push('/production/resources/manufacturing-Routes/');
    };

    const handleFieldChange = (propertyName, newValue) => {
        if (viewing()) {
            setEditStatus('edit');
        }
        setManufacturingRoute({ ...manufacturingRoute, [propertyName]: newValue });
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

                        <Fragment>
                            {console.info(manufacturingRoute.operations)}

                            {manufacturingRoute.operations ? (
                                <Table>
                                    <TableHead key="headers">
                                        <TableRow>
                                            <TableCell>Operation Number</TableCell>
                                            <TableCell>Description</TableCell>
                                            <TableCell>CIT Code</TableCell>
                                            <TableCell>Skill Code</TableCell>
                                            <TableCell>Set & Clean Time mins</TableCell>
                                            <TableCell>Resource Code</TableCell>
                                            <TableCell>Cycle Time mins </TableCell>
                                            <TableCell>Labour Percentage</TableCell>
                                            <TableCell>Links</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    {manufacturingRoute.operations.map((
                                        el //<p>{el.routeCode}</p>
                                    ) => (
                                        <Operation
                                            routeCode={el.routeCode}
                                            manufacturingId={el.manufacturingId}
                                            operationNumber={el.operationNumber}
                                            description={el.description}
                                            skillCode={el.skillCode}
                                            resourceCode={el.resourceCode}
                                            setAndCleanTime={el.setAndCleanTime}
                                            cycleTime={el.cycleTime}
                                            labourPercentage={el.labourPercentage}
                                            citCode={el.citCode}
                                            links={el.links}
                                            editStatus={editStatus}
                                        />
                                    ))}
                                </Table>
                            ) : (
                                <p>test</p>
                            )}
                        </Fragment>
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
        RouteCode: PropTypes.string,
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
    setSnackbarVisible: PropTypes.func.isRequired
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
