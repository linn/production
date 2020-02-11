import { connect } from 'react-redux';
import { getItemErrors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import buildPlanActions from '../../actions/buildPlanActions';
import buildPlanSelectors from '../../selectors/buildPlanSelectors';
import CreateBuildPlan from '../../components/buildPlans/CreateBuildPlan';

const mapStateToProps = state => ({
    loading: buildPlanSelectors.getLoading(state)
});

const mapDispatchToProps = {
    saveBuildPlan: buildPlanActions.add
};

export default connect(mapStateToProps, mapDispatchToProps)(CreateBuildPlan);
