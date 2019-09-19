import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import Grid from '@material-ui/core/Grid';
import { TableCell, TableRow } from '@material-ui/core';
import {
    SaveBackCancelButtons,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

function Operation({
    routeCode,
    manufacturingId,
    operationNumber,
    description,
    skillCode,
    resourceCode,
    setAndCleanTime,
    cycleTime,
    labourPercentage,
    citCode,
    links,
    editStatus
}) {
    const handleFieldChange = () => {
        alert('change');
    };

    const creating = () => editStatus === 'create';
    const editing = () => editStatus === 'edit';
    const viewing = () => editStatus === 'view';

    // const handleFieldChange = (propertyName, newValue) => {
    //     if (viewing()) {
    //         setEditStatus('edit');
    //     }
    //     setManufacturingRoute({ ...manufacturingRoute, [propertyName]: newValue });
    // };

    return (
        <Fragment>
            <TableRow>
                <TableCell>
                    {operationNumber}
                    {/* <InputField
                        value={operationNumber}
                        maxLength={50}
                        fullWidth
                        //helperText={opInvalid() ? 'This field is required' : ''}
                        required
                        onChange={handleFieldChange}
                        propertyName="operationNumber"
                    /> */}
                </TableCell>
                <TableCell>
                    <InputField
                        value={description}
                        maxLength={50}
                        fullWidth
                        //helperText={descriptionInvalid() ? 'This field is required' : ''}
                        required
                        onChange={handleFieldChange}
                        propertyName="description"
                    />{' '}
                </TableCell>
                <TableCell>
                    <InputField
                        value={citCode}
                        maxLength={50}
                        fullWidth
                        //elperText={citInvalid() ? 'This field is required' : ''}
                        required
                        onChange={handleFieldChange}
                        propertyName="Cit"
                    />{' '}
                </TableCell>
                <TableCell>
                    <InputField
                        value={skillCode}
                        maxLength={50}
                        fullWidth
                        //helperText={skillCodeInvalid() ? 'This field is required' : ''}
                        required
                        onChange={handleFieldChange}
                        propertyName="skillCode"
                    />{' '}
                </TableCell>
                <TableCell>
                    <InputField
                        value={setAndCleanTime}
                        maxLength={50}
                        fullWidth
                        //helperText={setAndCleanTimeInvalid() ? 'This field is required' : ''}
                        required
                        onChange={handleFieldChange}
                        propertyName="setAndCleanTimeMins"
                    />{' '}
                </TableCell>
                <TableCell>
                    <InputField
                        value={resourceCode}
                        maxLength={50}
                        fullWidth
                        //helperText={resourceCodeInvalid() ? 'This field is required' : ''}
                        required
                        onChange={handleFieldChange}
                        propertyName="resourceCode"
                    />{' '}
                </TableCell>
                <TableCell>
                    <InputField
                        value={cycleTime}
                        maxLength={50}
                        fullWidth
                        // helperText={cycleTimeInvalid() ? 'This field is required' : ''}
                        required
                        onChange={handleFieldChange}
                        propertyName="cycleTime"
                    />{' '}
                </TableCell>
                <TableCell>
                    <InputField
                        value={labourPercentage}
                        maxLength={50}
                        fullWidth
                        // helperText={labourPercentageInvalid() ? 'This field is required' : ''}
                        required
                        onChange={handleFieldChange}
                        propertyName="labourPercentage"
                    />
                </TableCell>
                <TableCell>
                    <InputField
                        value={links}
                        maxLength={50}
                        fullWidth
                        //   helperText={linksInvalid() ? 'This field is required' : ''}
                        required
                        onChange={handleFieldChange}
                        propertyName="links"
                    />
                </TableCell>
            </TableRow>
        </Fragment>
    );
}

Operation.propTypes = {
    routeCode: PropTypes.string.isRequired,
    manufacturingId: PropTypes.string.isRequired,
    operationNumber: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,
    skillCode: PropTypes.string.isRequired,
    resourceCode: PropTypes.string.isRequired,
    setAndCleanTime: PropTypes.string.isRequired,
    cycleTime: PropTypes.string.isRequired,
    labourPercentage: PropTypes.string.isRequired,
    citCode: PropTypes.string.isRequired,
    links: PropTypes.string.isRequired
};

Operation.defaultProps = {};

export default Operation;
