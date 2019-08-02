import React from 'react';
import Grid from '@material-ui/core/Grid';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import PropTypes from 'prop-types';
import TabBar from './TabBar';
import TabPanel from './TabPanel';

function ProductionMeasuresCits({ citsData }) {
    const [tabValue, setValue] = React.useState(0);

    function handleChange(event, newValue) {
        setValue(newValue);
    }

    return (
        <Grid container>
            <Grid item xs={4}>
                Cit
                {citsData ? (
                    <List>
                        {citsData.map(cit => (
                            <ListItem>{cit.citName}</ListItem>
                        ))}
                    </List>
                ) : (
                    'No'
                )}
            </Grid>
            <Grid item xs={8}>
                <TabBar value={tabValue} onChange={handleChange} />
                <TabPanel value={tabValue} index={0}>
                    <Table size="small">
                        <TableHead>
                            <TableRow>
                                <TableCell />
                                <TableCell />
                                <TableCell />
                                <TableCell align="centre" colSpan={2}>Days</TableCell>
                                <TableCell align="centre" colSpan={2}>Can Do</TableCell>                                
                                <TableCell />
                                <TableCell />
                            </TableRow>
                            <TableRow>
                                <TableCell>1s</TableCell>
                                <TableCell>2s</TableCell>
                                <TableCell>3s</TableCell>
                                <TableCell>1/2s</TableCell>
                                <TableCell>3s</TableCell>
                                <TableCell>1/2s</TableCell>
                                <TableCell>3s</TableCell>                                
                                <TableCell>4s</TableCell>
                                <TableCell>5s</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {citsData.map(m => (
                                <TableRow>
                                    <TableCell>{m.ones}</TableCell>
                                    <TableCell>{m.twos}</TableCell>
                                    <TableCell>{m.threes}</TableCell>
                                    <TableCell>{m.daysRequired}</TableCell>
                                    <TableCell>{m.daysRequired3}</TableCell>
                                    <TableCell>{m.daysRequiredCanDo12}</TableCell>
                                    <TableCell>{m.daysRequiredCanDo3}</TableCell>                                    
                                    <TableCell>{m.fours}</TableCell>
                                    <TableCell>{m.fives}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TabPanel>
                <TabPanel value={tabValue} index={1}>
                    Delivery Perf
                </TabPanel>
                <TabPanel value={tabValue} index={2}>
                    Shortages
                </TabPanel>
                <TabPanel value={tabValue} index={3}>
                    Back Orders
                </TabPanel>
                <TabPanel value={tabValue} index={4}>
                    Built This Week
                </TabPanel>
                <TabPanel value={tabValue} index={5}>
                    Stock
                </TabPanel>
            </Grid>
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
