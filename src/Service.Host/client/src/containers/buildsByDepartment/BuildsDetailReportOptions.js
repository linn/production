import { connect } from 'react-redux';
import { ReportSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import actions from '../../actions/departmentActions';
import departmentsSelectors from '../../selectors/departmentsSelectors';
import BuildsDetailReportOptions from '../../components/buildsbyDepartment/BuildsDetailReportOptions';
import * as reportTypes from '../../reportTypes';

const reportSelectors = new ReportSelectors(reportTypes.buildsDetailReport.item);

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    departments: departmentsSelectors.getItems(state),
    departmentsLoading: departmentsSelectors.getLoading(state),
    prevOptions: reportSelectors.getReportOptions(state),
    options: getOptions(ownProps)
});

const initialise = () => dispatch => {
    dispatch(actions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(BuildsDetailReportOptions));
