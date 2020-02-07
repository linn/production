import { connect } from 'react-redux';
import { getItemErrors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import buildPlansActions from '../../actions/buildPlansActions';
import buildPlansSelectors from '../../selectors/buildPlansSelectors';
import buildPlanDetailsActions from '../../actions/buildPlanDetailsActions';
import buildPlanDetailsSelectors from '../../selectors/buildPlanDetailsSelectors';
import buildPlanDetailActions from '../../actions/buildPlanDetailActions';
import buildPlanRulesActions from '../../actions/buildPlanRulesActions';
import buildPlanRulesSelectors from '../../selectors/buildPlanRulesSelectors';
import BuildPlans from '../../components/buildPlans/BuildPlans';
import partsActions from '../../actions/partsActions';
import partsSelectors from '../../selectors/partsSelectors';
import buildPlanActions from '../../actions/buildPlanActions';
import buildPlanDetailSelectors from '../../selectors/buildPlanDetailSelectors';
import buildPlanSelectors from '../../selectors/buildPlanSelectors';

const mapStateToProps = (state, { match }) => ({
    buildPlans: buildPlansSelectors.getItems(state),
    itemErrors: getItemErrors(state),
    buildPlansLoading: buildPlansSelectors.getLoading(state),
    buildPlanDetails: buildPlanDetailsSelectors.getItems(state),
    buildPlanDetailsLoading: buildPlanDetailsSelectors.getLoading(state),
    buildPlanRules: buildPlanRulesSelectors.getItems(state),
    buildPlanRulesLoading: buildPlanRulesSelectors.getLoading(state),
    partsSearchResults: partsSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.partNumber, name: s.partNumber })),
    partsSearchLoading: partsSelectors.getSearchLoading(state),
    selectedBuildPlan: match.params.id,
    selectedBuildPlanDetail: match.params.buildPlanDetail,
    buildPlanDetailLoading: buildPlanDetailSelectors.getLoading(state),
    buildPlanDetail: buildPlanDetailSelectors.getItem(state),
    buildPlanSnackbarVisible: buildPlanSelectors.getSnackbarVisible(state),
    buildPlanDetailSnackbarVisible: buildPlanDetailSelectors.getSnackbarVisible(state)
});

const initialise = () => dispatch => {
    dispatch(buildPlansActions.fetch());
    dispatch(buildPlanDetailsActions.fetch());
    dispatch(buildPlanRulesActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    searchParts: partsActions.search,
    clearPartsSearch: partsActions.clearSearch,
    updateBuildPlan: buildPlanActions.update,
    updateBuildPlanDetail: buildPlanDetailActions.update,
    saveBuildPlanDetail: buildPlanDetailActions.add,
    fetchBuildPlans: buildPlansActions.fetch,
    fetchBuildPlanDetails: buildPlanDetailsActions.fetch,
    fetchBuildPlanRules: buildPlanRulesActions.fetch,
    setBuildPlanSnackbarVisible: buildPlanActions.setSnackbarVisible,
    setBuildPlanDetailSnackbarVisible: buildPlanDetailActions.setSnackbarVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(BuildPlans));
