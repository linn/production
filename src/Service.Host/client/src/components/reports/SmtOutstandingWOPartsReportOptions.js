import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Dropdown, Title, InputField } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Typography from '@material-ui/core/Typography';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import ListItemText from '@material-ui/core/ListItemText';
import DeleteIcon from '@material-ui/icons/Delete';
import IconButton from '@material-ui/core/IconButton';
import ArrowForward from '@material-ui/icons/ArrowForward';
import Page from '../../containers/Page';

function SmtOutstandingWOPartsReportOptions({ history }) {
    const [smtLine, setSmtLine] = useState('All');
    const [partField, setPartField] = useState('');
    const [parts, setParts] = useState([]);

    const handleClick = () => {
        let search = `?smtLine=${smtLine}`;

        if (parts.length > 0) {
            search += '&parts=';
            search += parts.join('&parts=');
        }

        history.push({
            pathname: `/production/reports/smt/outstanding-works-order-parts/report`,
            search
        });
    };

    const handleDropdownChange = (_field, newValue) => {
        setSmtLine(newValue);
    };

    const addPart = newValue => {
        setParts([...parts, newValue ? newValue.toUpperCase() : '']);
        setPartField('');
    };

    const handleFieldChange = (_, newValue) => {
        setPartField(newValue);
    };

    return (
        <Page>
            <Title text="Parts needed for outstanding SMT works orders" />
            <Grid style={{ marginTop: 40 }} container spacing={3} justify="center">
                <Grid item xs={6}>
                    <Dropdown
                        label="SMT Line"
                        propertyName="smtLine"
                        items={[
                            { id: 'SMT1', displayText: 'SMT1' },
                            { id: 'SMT2', displayText: 'SMT2' },
                            { id: 'All', displayText: 'All' }
                        ]}
                        value={smtLine}
                        onChange={handleDropdownChange}
                    />
                </Grid>
                <Grid xs={6} />
                <Grid xs={12}>
                    <Typography variant="subtitle2">
                        Components to show on report - Leave blank to show all parts
                    </Typography>
                </Grid>
                <Grid item xs={3}>
                    <InputField
                        label="Part"
                        maxLength={14}
                        fullWidth
                        value={partField}
                        onChange={handleFieldChange}
                        propertyName="part"
                    />
                </Grid>
                <Grid xs={1}>
                    <IconButton
                        style={{ marginTop: 30 }}
                        aria-label="select"
                        onClick={() => addPart(partField)}
                    >
                        <ArrowForward fontSize="inherit" />
                    </IconButton>
                </Grid>
                <Grid xs={2} />
                <Grid xs={4}>
                    <Typography variant="subtitle2">Components selected</Typography>
                    <List dense>
                        {parts.map(p => (
                            <ListItem>
                                <ListItemText primary={p} />
                                <ListItemSecondaryAction>
                                    <IconButton edge="end" aria-label="delete">
                                        <DeleteIcon />
                                    </IconButton>
                                </ListItemSecondaryAction>
                            </ListItem>
                        ))}
                    </List>
                </Grid>
                <Grid xs={2} />
                <Grid item xs={12}>
                    <Button color="primary" variant="contained" onClick={handleClick}>
                        Run Report
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

SmtOutstandingWOPartsReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    prevOptions: PropTypes.shape({
        fromDate: PropTypes.string,
        toDate: PropTypes.string
    }).isRequired
};

export default SmtOutstandingWOPartsReportOptions;
