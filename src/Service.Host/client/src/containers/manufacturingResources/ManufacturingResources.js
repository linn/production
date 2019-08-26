import { connect } from 'react-redux';
import { fetchErrorSelectors, initialiseOnMount } from '@linn-it/linn-form-components-library';
import ViewManufacturingResources from '../../components/manufacturingResources/ManufacturingResources';
import manufacturingResourcesActions from '../../actions/manufacturingResourcesActions';
import manufacturingResourcesSelectors from '../../selectors/manufacturingResourcesSelectors';

const mapStateToProps = state => ({
    items: manufacturingResourcesSelectors.getItems(state),
    loading: manufacturingResourcesSelectors.getLoading(state),
    errorMessage: fetchErrorSelectors(state)
});

const initialise = () => dispatch => {
    console.info(manufacturingResourcesActions);
    dispatch(manufacturingResourcesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ViewManufacturingResources));
