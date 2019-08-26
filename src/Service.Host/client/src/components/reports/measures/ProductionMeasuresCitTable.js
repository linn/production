import React from 'react';
import Grid from '@material-ui/core/Grid';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import PropTypes from 'prop-types';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import LinkTabCell from './LinkTabCell';
import TabCell from './TabCell';

function ProductionMeasuresCits({ citsData, config }) {
    const [tabValue, setValue] = React.useState(0);
    const [hoverHref, setHoverHref] = React.useState(null);

    // TODO write these drill downs and check they are all still required
    const triggerBaseHref = `${config.appRoot}/production/reports/triggers`;
    const daysRequiredBaseHref = `${config.appRoot}/production/reports/measures/days-required`;
    const shortagesBaseHref = `${config.appRoot}/production/reports/measures/shortages`;
    const backOrdersBaseHref = `${config.appRoot}/production/reports/measures/back-orders`;
    const builtThisWeekBaseHref = `${config.appRoot}/production/reports/measures/built-this-week`;
    const fflagStockBaseHref = `${config.appRoot}/production/reports/stock/f-flag`; // may already exist in linnapps

    function handleChange(event, newValue) {
        setValue(newValue);
    }

    function round(num) {
        return num ? num.toFixed() : null;
    }

    return (
        <Grid xs={12}>
            <Table size="small">
                <TableHead>
                    <TableRow>
                        <TableCell />
                        <TableCell colSpan="9">
                            <AppBar position="static" color="default">
                                <Tabs
                                    value={tabValue}
                                    onChange={handleChange}
                                    indicatorColor="primary"
                                    textColor="primary"
                                    variant="scrollable"
                                    scrollButtons="auto"
                                    aria-label="osr-tabs"
                                >
                                    <Tab label="Trigger System" />
                                    <Tab label="Delivery Perf" />
                                    <Tab label="Shortages" />
                                    <Tab label="Back Orders" />
                                    <Tab label="Built This Week" />
                                    <Tab label="Stock" />
                                </Tabs>
                            </AppBar>
                        </TableCell>
                    </TableRow>
                    <TableRow>
                        <TabCell />
                        <TabCell index={0} value={tabValue} />
                        <TabCell index={0} value={tabValue} />
                        <TabCell index={0} value={tabValue} />
                        <TabCell index={0} value={tabValue} align="centre" colSpan={2}>
                            Days
                        </TabCell>
                        <TabCell index={0} value={tabValue} align="centre" colSpan={2}>
                            Can Do
                        </TabCell>
                        <TabCell index={0} value={tabValue} />
                        <TabCell index={0} value={tabValue} />
                        <TabCell index={1} value={tabValue} align="centre" colSpan={2}>
                            Delivery Perf%
                        </TabCell>
                        <TabCell index={1} value={tabValue} />
                        <TabCell index={2} value={tabValue} align="centre" colSpan={4}>
                            No of shortages (1/2s)
                        </TabCell>
                        <TabCell index={3} value={tabValue} align="centre" colSpan={3}>
                            Production Back Orders
                        </TabCell>
                        <TabCell index={4} value={tabValue} align="centre" colSpan={2}>
                            Built This Week
                        </TabCell>
                        <TabCell index={4} value={tabValue} />
                        <TabCell index={5} value={tabValue} align="centre" colSpan={3}>
                            Stock
                        </TabCell>
                    </TableRow>
                    <TableRow>
                        <TableCell>Cits</TableCell>
                        <TabCell index={0} value={tabValue}>
                            1s
                        </TabCell>
                        <TabCell index={0} value={tabValue}>
                            2s
                        </TabCell>
                        <TabCell index={0} value={tabValue}>
                            3s
                        </TabCell>
                        <TabCell index={0} value={tabValue}>
                            1/2s
                        </TabCell>
                        <TabCell index={0} value={tabValue}>
                            3s
                        </TabCell>
                        <TabCell index={0} value={tabValue}>
                            1/2s
                        </TabCell>
                        <TabCell index={0} value={tabValue}>
                            3s
                        </TabCell>
                        <TabCell index={0} value={tabValue}>
                            4s
                        </TabCell>
                        <TabCell index={0} value={tabValue}>
                            5s
                        </TabCell>
                        <TabCell index={1} value={tabValue}>
                            1s%
                        </TabCell>
                        <TabCell index={1} value={tabValue}>
                            2s%
                        </TabCell>
                        <TabCell index={1} value={tabValue} />
                        <TabCell index={2} value={tabValue}>
                            Shortages
                        </TabCell>
                        <TabCell index={2} value={tabValue}>
                            BAT
                        </TabCell>
                        <TabCell index={2} value={tabValue}>
                            Metalwork
                        </TabCell>
                        <TabCell index={2} value={tabValue}>
                            Procurement
                        </TabCell>
                        <TabCell index={3} value={tabValue} align="right">
                            Num
                        </TabCell>
                        <TabCell index={3} value={tabValue} align="right">
                            Value
                        </TabCell>
                        <TabCell index={3} value={tabValue} align="right">
                            Oldest
                        </TabCell>
                        <TabCell index={4} value={tabValue} align="right">
                            Value
                        </TabCell>
                        <TabCell index={4} value={tabValue} align="right">
                            Qty
                        </TabCell>
                        <TabCell index={4} value={tabValue} />
                        <TabCell index={5} value={tabValue} align="right">
                            F-Flag qty
                        </TabCell>
                        <TabCell index={5} value={tabValue} align="right">
                            Stock
                        </TabCell>
                        <TabCell index={5} value={tabValue} align="right">
                            Over-Stock
                        </TabCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {citsData.map(m => (
                        <TableRow>
                            <TableCell>{m.citName}</TableCell>
                            <LinkTabCell
                                index={0}
                                value={tabValue}
                                href={`${triggerBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {m.ones}
                            </LinkTabCell>
                            <LinkTabCell
                                index={0}
                                value={tabValue}
                                href={`${triggerBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {m.twos}
                            </LinkTabCell>
                            <LinkTabCell
                                index={0}
                                value={tabValue}
                                href={`${triggerBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {m.threes}
                            </LinkTabCell>
                            <LinkTabCell
                                index={0}
                                value={tabValue}
                                href={`${daysRequiredBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {round(m.daysRequired)}
                            </LinkTabCell>
                            <LinkTabCell
                                index={0}
                                value={tabValue}
                                href={`${daysRequiredBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {round(m.daysRequired3)}
                            </LinkTabCell>
                            <LinkTabCell
                                index={0}
                                value={tabValue}
                                href={`${daysRequiredBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {round(m.daysRequiredCanDo12)}
                            </LinkTabCell>
                            <LinkTabCell
                                index={0}
                                value={tabValue}
                                href={`${daysRequiredBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {round(m.daysRequiredCanDo3)}
                            </LinkTabCell>
                            <TabCell index={0} value={tabValue}>
                                {m.fours}
                            </TabCell>
                            <TabCell index={0} value={tabValue}>
                                {m.fives}
                            </TabCell>
                            <TabCell index={1} value={tabValue}>
                                {m.deliveryPerformance1s}
                            </TabCell>
                            <TabCell index={1} value={tabValue}>
                                {m.deliveryPerformance2s}
                            </TabCell>
                            <TabCell index={1} value={tabValue} />
                            <LinkTabCell
                                index={2}
                                value={tabValue}
                                href={`${shortagesBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {m.shortAny}
                            </LinkTabCell>
                            <LinkTabCell
                                index={2}
                                value={tabValue}
                                href={`${shortagesBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {m.shortBat}
                            </LinkTabCell>
                            <LinkTabCell
                                index={2}
                                value={tabValue}
                                href={`${shortagesBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {m.shortMetalwork}
                            </LinkTabCell>
                            <LinkTabCell
                                index={2}
                                value={tabValue}
                                href={`${shortagesBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {m.shortProc}
                            </LinkTabCell>
                            <LinkTabCell
                                index={3}
                                value={tabValue}
                                href={`${backOrdersBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                                align="right"
                            >
                                {m.numberOfPartsBackOrdered}
                            </LinkTabCell>
                            <LinkTabCell
                                index={3}
                                value={tabValue}
                                href={`${backOrdersBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                                align="right"
                            >
                                {m.backOrderValue}
                            </LinkTabCell>
                            <LinkTabCell
                                index={3}
                                value={tabValue}
                                href={`${backOrdersBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                                align="right"
                            >
                                {m.oldestBackOrder}
                            </LinkTabCell>
                            <LinkTabCell
                                index={4}
                                value={tabValue}
                                href={`${builtThisWeekBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {m.builtThisWeekQty}
                            </LinkTabCell>
                            <LinkTabCell
                                index={4}
                                value={tabValue}
                                href={`${builtThisWeekBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                            >
                                {m.builtThisWeekValue}
                            </LinkTabCell>
                            <LinkTabCell
                                index={5}
                                value={tabValue}
                                href={`${fflagStockBaseHref}?citCode=${m.citCode}`}
                                setHoverHref={setHoverHref}
                                hoverHref={hoverHref}
                                align="right"
                            >
                                {m.fFlaggedQty}
                            </LinkTabCell>
                            <TabCell index={5} value={tabValue} align="right">
                                {m.stockValue}
                            </TabCell>
                            <TabCell index={5} value={tabValue} align="right">
                                {m.overStockValue}
                            </TabCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </Grid>
    );
}

ProductionMeasuresCits.propTypes = {
    citsData: PropTypes.shape({})
};

ProductionMeasuresCits.defaultProps = {
    citsData: null
};

export default ProductionMeasuresCits;
