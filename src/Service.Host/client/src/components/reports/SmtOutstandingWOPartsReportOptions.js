import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import {
    Dropdown,
    Title,
    InputField,
    TypeaheadDialog,
    SelectedItemsList
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/styles';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import ArrowForward from '@material-ui/icons/ArrowForward';
import Page from '../../containers/Page';

function SmtOutstandingWOPartsReportOptions({
    history,
    partsSearchResults,
    partsSearchLoading,
    searchParts,
    clearPartsSearch
}) {
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

    const addTypeAheadPart = newValue => {
        setParts([...parts, newValue.partNumber]);
        clearPartsSearch();
    };

    const handleFieldChange = (_, newValue) => {
        setPartField(newValue);
    };

    const removePart = part => {
        setParts(parts.filter(p => p !== part));
    };

    const useStyles = makeStyles(theme => ({
        marginTop: {
            marginTop: theme.spacing(3)
        }
    }));
    const classes = useStyles();

    return (
        <Page>
            <Title text="Parts needed for outstanding SMT works orders" />
            <Grid className={classes.marginTop} container spacing={3} justify="center">
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
                <Grid item xs={6} />
                <Grid item xs={12}>
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
                <Grid item xs={1}>
                    <div className={classes.marginTop}>
                        <TypeaheadDialog
                            title="Search For Part"
                            onSelect={addTypeAheadPart}
                            searchItems={partsSearchResults}
                            loading={partsSearchLoading}
                            fetchItems={searchParts}
                            clearSearch={() => clearPartsSearch}
                        />
                    </div>
                </Grid>
                <Grid item xs={1}>
                    <Tooltip title="Add part from input field">
                        <IconButton
                            className={classes.marginTop}
                            aria-label="select"
                            onClick={() => addPart(partField)}
                        >
                            <ArrowForward fontSize="inherit" />
                        </IconButton>
                    </Tooltip>
                </Grid>
                <Grid item xs={1} />
                <Grid item xs={2}>
                    <SelectedItemsList
                        title="Components selected"
                        items={parts}
                        removeItem={removePart}
                    />
                </Grid>
                <Grid item xs={4} />
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
    }).isRequired,
    partsSearchLoading: PropTypes.bool,
    partsSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    searchParts: PropTypes.func.isRequired,
    clearPartsSearch: PropTypes.func.isRequired
};

SmtOutstandingWOPartsReportOptions.defaultProps = {
    partsSearchResults: [],
    partsSearchLoading: false
};

export default SmtOutstandingWOPartsReportOptions;
