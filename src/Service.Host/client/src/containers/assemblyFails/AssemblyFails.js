import { connect } from 'react-redux';
import { getPreviousPaths } from '@linn-it/linn-form-components-library';
import AssemblyFails from '../../components/assemblyFails/AssemblyFails';
import assemblyFailsActions from '../../actions/assemblyFailsActions';
import assemblyFailsSelectors from '../../selectors/assemblyFailsSelectors';

const mapStateToProps = state => ({
    items: assemblyFailsSelectors.getSearchItems(state),
    loading: assemblyFailsSelectors.getSearchLoading(state),
    previousPaths: getPreviousPaths(state)
});

const mapDispatchToProps = {
    fetchItems: assemblyFailsActions.search,
    searchWithOptions: assemblyFailsActions.searchWithOptions,
    clearSearch: assemblyFailsActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(AssemblyFails);
