import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { Dropdown, Title } from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function SmtOutstandingWOPartsReportOptions({ history }) {
    const [smtLine, setSmtLine] = useState('All');

    const handleClick = () =>
        history.push({
            pathname: `/production/reports/smt/outstanding-works-order-parts/report`,
            search: `?smtLine=${smtLine}`
        });

    const handleDropdownChange = (_field, newValue) => {
        setSmtLine(newValue);
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
