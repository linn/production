import React, { useState, useEffect } from 'react';
import { Loading, Title, Dropdown } from '@linn-it/linn-form-components-library';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import PropTypes from 'prop-types';
import Page from '../../containers/Page';

function OutstandingWorksOrdersReportOptions({ cits, citsLoading, history }) {
    const [options, setOptions] = useState({ reportType: 'All', cit: '' });
    const [citOptions, setCitOptions] = useState([]);

    useEffect(() => {
        if (cits !== null) {
            const citsFormatted = cits.map(cit => ({
                id: cit.code,
                displayText: cit.name
            }));

            setCitOptions([{ id: '', displayText: '' }, ...citsFormatted]);
        }
    }, [cits]);

    const filterOptions = ['All', 'CIT'];

    const handleClick = () => {
        const queryString =
            options.reportType === 'All' ? '' : `?reportType=cit&searchParameter=${options.cit}`;

        history.push({
            pathname: '/production/works-orders/outstanding-works-orders-report/report',
            search: queryString
        });
    };

    const handleFieldChange = (propertyName, newValue) =>
        setOptions(o => ({ ...o, [propertyName]: newValue }));

    return (
        <Page>
            <Title text="Outstanding Works Orders Report" />
            {citsLoading ? (
                <Loading />
            ) : (
                <Grid container spacing={3}>
                    <Grid item xs={4}>
                        <Dropdown
                            items={filterOptions}
                            label="Report Type"
                            propertyName="reportType"
                            onChange={handleFieldChange}
                            value={options.reportType}
                        />
                    </Grid>
                    <Grid item xs={8} />
                    {options.reportType === 'CIT' && (
                        <>
                            <Grid item xs={4}>
                                <Dropdown
                                    label="CITs"
                                    propertyName="cit"
                                    items={citOptions}
                                    value={options.cit || ''}
                                    onChange={handleFieldChange}
                                />
                            </Grid>
                            <Grid item xs={8} />
                        </>
                    )}
                    <Grid item xs={12}>
                        <Button color="primary" variant="contained" onClick={handleClick}>
                            Run Report
                        </Button>
                    </Grid>
                </Grid>
            )}
        </Page>
    );
}

OutstandingWorksOrdersReportOptions.propTypes = {
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    cits: PropTypes.arrayOf(PropTypes.shape({})),
    citsLoading: PropTypes.bool
};

OutstandingWorksOrdersReportOptions.defaultProps = {
    cits: [],
    citsLoading: false
};

export default OutstandingWorksOrdersReportOptions;
