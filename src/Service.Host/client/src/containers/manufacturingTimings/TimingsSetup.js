import { connect } from 'react-redux';
import { initialiseOnMount, ReportSelectors } from '@linn-it/linn-form-components-library';
import TimingsSetup from '../../components/manufacturingTimings/TimingsSetup';
import * as reportTypes from '../../reportTypes';
import citsActions from '../../actions/citsActions';
import citsSelectors from '../../selectors/citsSelectors';

const reportSelectors = new ReportSelectors(reportTypes.manufacturingTimingsReport.item);

const mapStateToProps = state => ({
    prevOptions: reportSelectors.getReportOptions(state),
    cits: citsSelectors.getItems(state),
    citsLoading: citsSelectors.getLoading(state)
});

const initialise = () => dispatch => {
    dispatch(citsActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(TimingsSetup));
