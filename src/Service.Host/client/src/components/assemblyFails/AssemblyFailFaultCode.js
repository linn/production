import React, { useEffect, useState, Fragment } from 'react';
import PropTypes from 'prop-types';
import EditIcon from '@material-ui/icons/Edit';
import Clear from '@material-ui/icons/Clear';
import Done from '@material-ui/icons/Done';
import Button from '@material-ui/core/Button';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import { DatePicker, InputField } from '@linn-it/linn-form-components-library';
import moment from 'moment';
import { makeStyles } from '@material-ui/styles';

const useStyles = makeStyles(theme => ({
    button: {
        maxWidth: theme.spacing(5),
        minWidth: theme.spacing(5),
        maxHeight: theme.spacing(3),
        minHeight: theme.spacing(3),
        padding: 0
    }
}));

export default function AssemblyFailFaultCode({
    item,
    updateAssemblyFailFaultCode,
    creating,
    removeItem,
    key
}) {
    const [editing, setEditing] = useState(false);
    const [prevItem, setPrevItem] = useState({});
    const [faultCode, setFaultCode] = useState({});

    const classes = useStyles();

    useEffect(() => {
        if (item !== prevItem) {
            setFaultCode(item);
            setPrevItem(item);
        }
        if (creating && item !== prevItem) {
            setEditing(true);
        }
    }, [item, prevItem, creating]);

    const handleFieldChange = (propertyName, newValue) => {
        setFaultCode({ ...faultCode, [propertyName]: newValue });
    };

    const handleSaveClick = () => {
        console.log('SAVE ME PLZ');
    };

    const handleCancelClick = () => {
        setEditing(false);
        if (creating) {
            removeItem();
            return;
        }
        if (prevItem) {
            setFaultCode(prevItem);
        }
    };

    return (
        <TableRow key={faultCode.faultCode}>
            {creating ? (
                <TableCell>
                    <InputField
                        fullWidth
                        maxLength={50}
                        propertyName="faultCode"
                        value={faultCode.faultCode}
                        onChange={handleFieldChange}
                        error={!faultCode.faultCode}
                    />
                </TableCell>
            ) : (
                <TableCell>{faultCode.faultCode}</TableCell>
            )}
            {editing ? (
                <Fragment>
                    <TableCell>
                        <InputField
                            fullWidth
                            maxLength={50}
                            propertyName="description"
                            value={faultCode.description}
                            onChange={handleFieldChange}
                        />
                    </TableCell>
                    <TableCell>
                        <InputField
                            fullWidth
                            maxLength={2000}
                            propertyName="explanation"
                            value={faultCode.explanation}
                            onChange={handleFieldChange}
                        />
                    </TableCell>
                    <TableCell>
                        <DatePicker
                            value={faultCode.dateInvalid}
                            onChange={value => {
                                handleFieldChange('dateInvalid', value);
                            }}
                        />
                    </TableCell>
                    <TableCell>
                        <Button
                            // TODO creating state that sets all to be editable
                            disabled={creating && !faultCode.faultCode}
                            onClick={handleSaveClick}
                            color="primary"
                            variant="outlined"
                            size="small"
                            classes={{
                                root: classes.button
                            }}
                        >
                            <Done fontSize="small" />
                        </Button>
                    </TableCell>
                    <TableCell>
                        <Button
                            onClick={handleCancelClick}
                            color="secondary"
                            variant="outlined"
                            classes={{
                                root: classes.button
                            }}
                            size="small"
                        >
                            <Clear fontSize="small" />
                        </Button>
                    </TableCell>
                </Fragment>
            ) : (
                <Fragment>
                    <TableCell>{faultCode.description}</TableCell>
                    <TableCell>{faultCode.explanation}</TableCell>
                    <TableCell>
                        {faultCode.dateInvalid
                            ? moment(faultCode.dateInvalid).format('DD MMM YYYY')
                            : ''}
                    </TableCell>
                    <TableCell>
                        <Button
                            color="primary"
                            aria-label="Edit"
                            variant="outlined"
                            onClick={() => setEditing(true)}
                            size="small"
                            classes={{
                                root: classes.button
                            }}
                        >
                            <EditIcon fontSize="small" />
                        </Button>
                    </TableCell>
                    <TableCell />
                </Fragment>
            )}
        </TableRow>
    );
}
